using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Configuration = GameOfVlad.GameObjects.UI.Components.Game.HealthBar.Configuration;

namespace GameOfVlad.GameObjects.UI.Components.Game;

public class HealthBar(ContentManager contentManager, Configuration configuration)
    : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.FrontCanvas;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _hearts;
        set => throw new NotSupportedException();
    }

    public override Size Size
    {
        get
        {
            int totalHearts = _hearts.Count;
            
            int rows = (int)Math.Ceiling((double)totalHearts / MaxHeartsInRow);
            int heartsInLastRow = totalHearts % MaxHeartsInRow;
            if (heartsInLastRow == 0 && totalHearts > 0)
            {
                heartsInLastRow = MaxHeartsInRow;
            }
            
            float heartWidth = _fullHeartTexture.Width; 
            float heartHeight = _fullHeartTexture.Height;
            
            float width = heartsInLastRow * (heartWidth + HeartSpacing) - HeartSpacing;
            float height = rows * (heartHeight + RowSpacing) - RowSpacing;
            
            return Size.Create(width, height);
        }
        set => throw new NotSupportedException();
    }

    private const int HeartEachHp = 25;
    private const int MaxHeartsInRow = 10; 
    private const int HeartSpacing = 5; 
    private const int RowSpacing = 5; 

    private readonly List<Heart> _hearts = new();

    private Texture2D _fullHeartTexture;
    private Texture2D _halfHeartTexture;
    private Texture2D _emptyHeartTexture;

    private int _currentHp;

    protected override void LoadCore()
    {
        _fullHeartTexture = this.ContentManager.Load<Texture2D>("2025/GameUI/HealthBar/health-bar-full-heart-32x32");
        _halfHeartTexture = this.ContentManager.Load<Texture2D>("2025/GameUI/HealthBar/health-bar-half-heart-32x32");
        _emptyHeartTexture = this.ContentManager.Load<Texture2D>("2025/GameUI/HealthBar/health-bar-empty-heart-32x32");
        _currentHp = configuration.GetCurrentHp();
        
        this.Position = this.CameraService.TopRightCorner(this.Size, -30, 30);
        
        CreateHearts();
        
        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        this.Position = this.CameraService.TopRightCorner(this.Size, -30, 30);

        int currentHp = configuration.GetCurrentHp();
        if (currentHp != _currentHp)
        {
            _currentHp = currentHp;
            UpdateHeartTextures();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
    
    private void CreateHearts()
    {
        int heartsCount = (int)Math.Ceiling((double)configuration.MaxHp / HeartEachHp);

        for (int i = 0; i < heartsCount; i++)
        {
            var heart = new Heart(this.ContentManager)
            {
                Parent = this
            };
            _hearts.Add(heart);
        }

        UpdateHeartPositions();
        UpdateHeartTextures();
    }

    private void UpdateHeartPositions()
    {
        float heartWidth = _fullHeartTexture.Width; 
        float heartHeight = _fullHeartTexture.Height;

        for (int i = 0; i < _hearts.Count; i++)
        {
            int row = i / MaxHeartsInRow;
            int column = i % MaxHeartsInRow;
            
            var position = new Vector2(
                column * (heartWidth + HeartSpacing),
                row * (heartHeight + RowSpacing)
            );
            
            _hearts[i].Position = position;
        }
    }

    private void UpdateHeartTextures()
    {
        int hp = _currentHp;

        foreach (Heart heart in _hearts)
        {
            if (hp >= HeartEachHp)
            {
                heart.Texture = _fullHeartTexture;
                hp -= HeartEachHp;
            }
            else if (hp > 0)
            {
                heart.Texture = _halfHeartTexture;
                hp = 0;
            }
            else
            {
                heart.Texture = _emptyHeartTexture;
            }
        }
    }

    public record Configuration(int MaxHp, Func<int> GetCurrentHp);

    private class Heart(ContentManager contentManager) : UiComponent(contentManager), IUiComponent
    {
        public int DrawOrder => (int)DrawOrderType.FrontCanvas;
        public int UpdateOrder => 1;
    }
}
