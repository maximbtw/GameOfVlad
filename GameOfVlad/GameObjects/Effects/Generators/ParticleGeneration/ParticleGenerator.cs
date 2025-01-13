using System;
using GameOfVlad.Utils;
using GameOfVlad.Utils.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects.Generators.ParticleGeneration;

public class ParticleGenerator(IEffectDrawer effectDrawer, ParticleGeneratorConfiguration configuration) : GameObject, IGameObject
{
    public int DrawOrder => (int)DrawOrderType.Effect;
    public int UpdateOrder => 1;
    
    private readonly Random _random = new();


    private float _timeSinceLastSpawn;

    public override void Update(GameTime gameTime)
    {
        _timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (!configuration.CanProduceParticle())
        {
            return;
        }
        
        while (_timeSinceLastSpawn >= 1f / configuration.SpawnRate)
        {
            _timeSinceLastSpawn -= 1f / configuration.SpawnRate;
            
            Particle particle = CreateParticle();
            effectDrawer.AddEffect(particle);
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    private Particle CreateParticle()
    {
        Vector2 baseDirection = configuration.GetDirection();

        Vector2 direction =
            GameHelper.GetOffsetDirectionByRandom(_random, baseDirection, configuration.OffsetAngleRange);

        float rotationSpeed = _random.Next(configuration.RotationSpeedRange.MinValue,
            configuration.RotationSpeedRange.MaxValue);

        Vector2 scale = Vector2.One * (configuration.ScaleRange.MinValue + (float)_random.NextDouble() *
            (configuration.ScaleRange.MaxValue - configuration.ScaleRange.MinValue));

        int speed = _random.Next(configuration.SpeedRange.MinValue, configuration.SpeedRange.MaxValue);

        Texture2D texture = configuration.Textures[_random.Next(0, configuration.Textures.Length)];
        Vector2 position = configuration.GetSpawnPosition();

        var particle = new Particle
        {
            Position = GameHelper.AdjustPositionByTexture(position, texture),
            Velocity = direction * speed,
            RotationSpeed = rotationSpeed,
            Scale = scale,
            Lifetime = configuration.ParticleLifetime,
            Texture = texture,
            Color = configuration.Colors[_random.Next(0, configuration.Colors.Count)],
            Parent = this
        };

        return particle;
    }

    private class Particle : GameObject, IEffect
    {
        public int DrawOrder => (int)DrawOrderType.Player;
        public int UpdateOrder => 1;

        public Vector2 Velocity { get; init; }
        public float RotationSpeed { get; init; }
        public float Lifetime { get; init; }

        private readonly Timer _lifetimeTimer = new();

        public override void Update(GameTime gameTime)
        {
            _lifetimeTimer.Update(gameTime);
            if (_lifetimeTimer.Time >= Lifetime)
            {
                Destroy();
            }
            
            this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Rotation += this.RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Color *= 1f - _lifetimeTimer.Time / Lifetime;

            base.Update(gameTime);
        }
    }
}
