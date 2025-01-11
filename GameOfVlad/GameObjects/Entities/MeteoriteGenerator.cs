using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.GameObjects.UI.Effects;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities;

public class MeteoriteGenerator(ContentManager contentManager, Rectangle levelBounds) : GameObject, IGameObject
{
    public int DrawOrder => (int)DrawOrderType.Effect;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _meteorites;
        set => throw new NotSupportedException();
    }
    
    public int SpawnFrequency { get; set; } = 75;
    public Range<int> MeteoriteSpeedRange { get; set; } = Range<int>.Create(200, 500);
    public Range<float> MeteoriteScaleRange { get; set; } = Range<float>.Create(1, 1);

    private const int SpawnOffset = 1000;

    private readonly Random _random = new();
    private readonly List<Meteorite> _meteorites = new();
    private readonly Texture2D[] _texture =
    [
        contentManager.Load<Texture2D>("Sprite/Meteorit/Meteorit")
    ];

    private TickUpdater _spawnUpdater;

    protected override void LoadCore()
    {
        _spawnUpdater = new TickUpdater(this.SpawnFrequency, () =>
        {
            Meteorite star = GenerateMeteorite();
            _meteorites.Add(star);
        });
        
        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        _spawnUpdater.Update();
        
        Console.WriteLine(_meteorites.Count);

        _meteorites.RemoveAll(star => star.Destroyed);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Meteorite GenerateMeteorite()
    {
        Vector2 position = PositionHelper.GeneratePositionBehindLevel(_random, levelBounds, SpawnOffset);

        // Рассчитываем направление внутрь уровня
        var levelCenter =
            new Vector2(levelBounds.X + levelBounds.Width / 2, levelBounds.Y + levelBounds.Height / 2);
        
        Vector2 directionToCenter = levelCenter - position;
        directionToCenter.Normalize();

        Vector2 rotatedDirection =
            GameHelper.GetOffsetDirectionByRandom(_random, directionToCenter, Range<int>.Create(-5, 5));
        
        float rotationSpeed = -2f+ (float)_random.NextDouble() * (2f - -2f);

        Vector2 scale = Vector2.One * (MeteoriteScaleRange.MinValue + (float)_random.NextDouble() *
            (MeteoriteScaleRange.MaxValue - MeteoriteScaleRange.MinValue));
        
        int speed = _random.Next(MeteoriteSpeedRange.MinValue, MeteoriteSpeedRange.MaxValue);
        Vector2 velocity = rotatedDirection * speed;
        
        var meteorite = new Meteorite(contentManager, levelBounds)
        {
            Texture = _texture[_random.Next(0, _texture.Length)],
            Position = position,
            Velocity = velocity,
            Scale = scale,
            RotationSpeed = rotationSpeed,
            Parent = this,
        };

        return meteorite;
    }

    private class Meteorite(ContentManager contentManager, Rectangle levelBounds) : ColliderGameObject, IColliderGameObject
    {
        public int DrawOrder => (int)DrawOrderType.Effect;
        public int UpdateOrder => 1;
        
        public override IEnumerable<IRendererObject> ChildrenBefore 
        {
            get
            {
                yield return _fireEffectParticleGenerator;
            }
            set => throw new NotSupportedException();
        }
        
        public Vector2 Velocity { get; set; }
        public float RotationSpeed { get; init; }

        private ParticleGenerator _fireEffectParticleGenerator;

        protected override void LoadCore()
        {
            _fireEffectParticleGenerator = new ParticleGenerator(new ParticleGenerator.Configuration(contentManager)
            {
                CanProduceParticle = () => true,
                GetSpawnPosition = () => this.DrawPosition,
                GetDirection = () => GameHelper.GetDirectionByVelocity(this.Velocity) * -1,
                SpawnRate = 25,
                ParticleLifetime = 1,
                SpeedRange = Range<int>.Create(500, 1000),
                OffsetAngleRange = Range<int>.Create(-10, 10),
                Colors = [Color.White, Color.Orange, ],
                ScaleRange = Range<float>.Create(0.8f * this.Scale.X, 1f * this.Scale.X),// Как парвило x == y
                RotationSpeedRange = Range<int>.Create(-20,20)
            })
            {
                Parent = this
            };
            
            base.LoadCore();
        }
        

        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Rotation += this.RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool needToDestroy = PositionHelper.IsPositionBehindLevel(this.Position, levelBounds, SpawnOffset);

            if (needToDestroy)
            {
                Destroyed = true;
            }

            base.Update(gameTime);
        }

        public void OnCollision(IColliderGameObject other)
        {
            
        }

        public void OnCollisionEnter(IColliderGameObject other)
        {
            if (other is PlayerV2)
            {
                this.Destroyed = true;
            }
        }

        public void OnCollisionExit(IColliderGameObject other)
        {
    
        }
    }
}