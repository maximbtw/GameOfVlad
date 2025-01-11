using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Configuration = GameOfVlad.GameObjects.UI.Effects.ParticleGenerator.Configuration;

namespace GameOfVlad.GameObjects.UI.Effects;

public class ParticleGenerator(Configuration configuration) : GameObject, IGameObject
{
    public int DrawOrder => (int)DrawOrderType.Player;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter 
    {
        get => _particles;
        set => throw new NotSupportedException();
    }

    private readonly List<Particle> _particles = new();
    private readonly Random _random = new();

    private float _timeSinceLastSpawn;

    public override void Update(GameTime gameTime)
    {
        _timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        _particles.RemoveAll(p => p.Destroyed);
        
        if (!configuration.CanProduceParticle())
        {
            return;
        }
        
        while (_timeSinceLastSpawn >= 1f / configuration.SpawnRate)
        {
            _timeSinceLastSpawn -= 1f / configuration.SpawnRate;
            
            Particle particle = CreateParticle();
            _particles.Add(particle);
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Particle CreateParticle()
    {
        Vector2 baseDirection = configuration.GetDirection();

        Vector2 direction = GetOffsetDirection(baseDirection, configuration.OffsetAngleRange);

        float rotationSpeed = _random.Next(configuration.RotationSpeedRange.MinValue,
            configuration.RotationSpeedRange.MaxValue);

        Vector2 scale = Vector2.One * (configuration.ScaleRange.MinValue + (float)_random.NextDouble() *
            (configuration.ScaleRange.MaxValue - configuration.ScaleRange.MinValue));

        int speed = _random.Next(configuration.SpeedRange.MinValue, configuration.SpeedRange.MaxValue);

        Texture2D texture = configuration.Textures[_random.Next(0, configuration.Textures.Count)];
        Vector2 position = configuration.GetSpawnPosition();

        var particle = new Particle
        {
            Position = GameHelper.AdjustPositionByTexture(position, texture),
            Velocity = direction * speed,
            RotationSpeed = rotationSpeed,
            Scale = scale,
            LifetimeFrequency = configuration.ParticleLifetime * 60,
            Texture = texture,
            Color = configuration.Colors[_random.Next(0, configuration.Colors.Count)],
            Parent = this
        };

        return particle;
    }

    private Vector2 GetOffsetDirection(Vector2 direction, Range<int> offsetAngle)
    {
        float randomAngleInDegrees = _random.Next(offsetAngle.MinValue, offsetAngle.MaxValue + 1);
        float randomAngleInRadians = GameHelper.AngleToRadians(randomAngleInDegrees);
        
        float cos = MathF.Cos(randomAngleInRadians);
        float sin = MathF.Sin(randomAngleInRadians);
        
        return new Vector2(
            direction.X * cos - direction.Y * sin,
            direction.X * sin + direction.Y * cos
        );
    }

    public class Configuration(ContentManager contentManager)
    {
        public Func<Vector2> GetSpawnPosition { get; set; }
        public Func<bool> CanProduceParticle { get; set; } = () => true;
        public Func<Vector2> GetDirection { get; set; } = () => Vector2.Zero;
        public float SpawnRate { get; set; } = 10;
        public int ParticleLifetime { get; set; } = 10;
        public List<Color> Colors { get; set; } = [Color.White];

        public Range<int> OffsetAngleRange { get; set; } = Range<int>.Create(-20, 20);
        public Range<int> SpeedRange { get; set; } = Range<int>.Create(10, 100);
        public Range<int> RotationSpeedRange { get; set; } = Range<int>.Create(-5, 5);
        public Range<float> ScaleRange { get; set; } = Range<float>.Create(0.5f, 1.5f);

        public List<Texture2D> Textures
        {
            get => _textures.Count != 0 ? _textures : _defaultTextures;
            set => _textures.AddRange(value);
        }

        private readonly List<Texture2D> _textures = new();

        private readonly List<Texture2D> _defaultTextures =
        [
            contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect1"),
            contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect2"),
            contentManager.Load<Texture2D>("Sprite/Meteorit/ParticleEffect3")
        ];
    }

    private class Particle : GameObject, IGameObject
    {
        public int DrawOrder => (int)DrawOrderType.Player;
        public int UpdateOrder => 1;

        public Vector2 Velocity { get; init; }
        public float RotationSpeed { get; init; }
        public int LifetimeFrequency { get; init; }

        private TickUpdater _lifetimeUpdater;

        protected override void LoadCore()
        {
            _lifetimeUpdater = new TickUpdater(this.LifetimeFrequency, () => this.Destroyed = true);
            base.LoadCore();
        }

        public override void Update(GameTime gameTime)
        {
            _lifetimeUpdater.Update();
            
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Rotation += this.RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Color *= 1f - (float)_lifetimeUpdater.Tick / LifetimeFrequency;

            base.Update(gameTime);
        }
    }
}
