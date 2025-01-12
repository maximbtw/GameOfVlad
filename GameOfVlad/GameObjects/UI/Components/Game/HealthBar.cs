using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
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

    public const int HeartEachHp = 25;
    public const int MaxHeartsInRow = 10; 
    public const int HeartSpacing = 35; 
    private const int RowSpacing = 35; 

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
        CreateHearts();

        base.LoadCore();
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
        for (int i = 0; i < _hearts.Count; i++)
        {
            int row = i / MaxHeartsInRow;
            int column = i % MaxHeartsInRow;

            int x = -((MaxHeartsInRow * HeartSpacing) - HeartSpacing) + column * HeartSpacing;
            int y = row * ( RowSpacing);

            _hearts[i].Position = new Vector2(this.Position.X + x, y);
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

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

    public record Configuration(int MaxHp, Func<int> GetCurrentHp);

    private class Heart(ContentManager contentManager) : UiComponent(contentManager), IUiComponent
    {
        public int DrawOrder => (int)DrawOrderType.FrontCanvas;
        public int UpdateOrder => 1;
    }
}
