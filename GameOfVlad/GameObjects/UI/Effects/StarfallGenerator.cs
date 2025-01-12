using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Effects;

public class StarfallGenerator(ContentManager contentManager, Rectangle levelBounds)
    : UiComponent(contentManager), IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.Background;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _stars;
        set => throw new NotSupportedException();
    }
    
    public int SpawnFrequency { get; set; } = 5;
    public Range<int> StarSpeedRange { get; set; } = Range<int>.Create(100, 1000);

    private const int SpawnOffset = 2000;

    private readonly Random _random = new();
    private readonly List<Star> _stars = new();
    private readonly Texture2D[] _texture =
    [
        contentManager.Load<Texture2D>("Sprite/Stars/1"),
        contentManager.Load<Texture2D>("Sprite/Stars/2"),
        contentManager.Load<Texture2D>("Sprite/Stars/3"),
        contentManager.Load<Texture2D>("Sprite/Stars/4")
    ];

    private TickUpdater _spawnUpdater;

    protected override void LoadCore()
    {
        _spawnUpdater = new TickUpdater(this.SpawnFrequency, () =>
        {
            Star star = GenerateStar();
            _stars.Add(star);
        });
        
        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        _spawnUpdater.Update();

        _stars.RemoveAll(star => star.Destroyed);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Star GenerateStar()
    {
        Vector2 position = PositionHelper.GeneratePositionBehindLevel(_random, levelBounds, SpawnOffset);

        // Рассчитываем направление внутрь уровня
        var levelCenter =
            new Vector2(levelBounds.X + levelBounds.Width / 2, levelBounds.Y + levelBounds.Height / 2);
        
        Vector2 directionToCenter = levelCenter - position;
        directionToCenter.Normalize();
        
        Vector2 rotatedDirection =
            GameHelper.GetOffsetDirectionByRandom(_random, directionToCenter, Range<int>.Create(-5, 5));
        
        int speed = _random.Next(StarSpeedRange.MinValue, StarSpeedRange.MaxValue);
        Vector2 velocity = rotatedDirection * speed;
        
        var star = new Star(this.ContentManager, levelBounds)
        {
            Texture = _texture[_random.Next(0, _texture.Length)],
            Position = position,
            Velocity = velocity,
            Parent = this,
        };

        return star;
    }
    

    private class Star(ContentManager contentManager, Rectangle levelBounds) : UiComponent(contentManager), IUiComponent
    {
        public int DrawOrder => (int)DrawOrderType.Background;
        public int UpdateOrder => 1;

        public override Vector2 DrawPosition => this.Position;
        public Vector2 Velocity { get; set; } 

        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool needToDestroy = PositionHelper.IsPositionBehindLevel(this.Position, levelBounds, SpawnOffset);

            if (needToDestroy)
            {
                Destroyed = true;
            }

            base.Update(gameTime);
        }
    }
}