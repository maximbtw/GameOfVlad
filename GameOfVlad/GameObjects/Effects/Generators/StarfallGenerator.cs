using System;
using GameOfVlad.GameObjects.UI;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects.Generators;

public class StarfallGenerator(ContentManager contentManager, IEffectDrawer effectDrawer, Rectangle levelBounds)
    : UiComponent(contentManager), IUiComponent
{
    public int UpdateOrder => 1;
    
    public int SpawnFrequency { get; set; } = 10;
    public Range<int> StarSpeedRange { get; set; } = Range<int>.Create(100, 1000);

    private const int SpawnOffset = 2000;

    private readonly Random _random = new();
    private readonly Texture2D[] _textures =
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
            
            effectDrawer.AddEffect(star);
        });
        
        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        _spawnUpdater.Update();

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Star GenerateStar()
    {
        Vector2 position = LevelHelper.GeneratePositionBehindLevel(_random, levelBounds, SpawnOffset);

        // Рассчитываем направление внутрь уровня
        var levelCenter =
            // ReSharper disable PossibleLossOfFraction
            new Vector2(levelBounds.X + levelBounds.Width / 2, levelBounds.Y + levelBounds.Height / 2);
        
        Vector2 directionToCenter = levelCenter - position;
        directionToCenter.Normalize();
        
        Vector2 rotatedDirection =
            GameHelper.GetOffsetDirectionByRandom(_random, directionToCenter, Range<int>.Create(-5, 5));
        
        int speed = _random.Next(StarSpeedRange.MinValue, StarSpeedRange.MaxValue);
        Vector2 velocity = rotatedDirection * speed;
        
        var star = new Star(levelBounds)
        {
            Texture = _textures[_random.Next(0, _textures.Length)],
            Position = position,
            Velocity = velocity,
            Parent = this,
        };

        return star;
    }
    

    private class Star(Rectangle levelBounds) : GameObject, IEffect
    {
        public override float LayerDepth => (float)DrawOrderType.BackEffect / 100f;
        public int UpdateOrder => 1;
        
        public override Vector2 CenterPosition => this.Position;
        public Vector2 Velocity { get; set; } 

        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool needToDestroy = LevelHelper.IsPositionBehindLevel(this.Position, levelBounds, SpawnOffset);

            if (needToDestroy)
            {
                Destroyed = true;
            }

            base.Update(gameTime);
        }
    }
}