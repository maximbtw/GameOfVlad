using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameEffects
{
    public class HitBulletEffect : ParticleEngine
    {
        public bool End = false;
        private float timeDuration = 0;
        public HitBulletEffect(List<Texture2D> textures, Vector2 location)
                 : base(textures, location)
        {
            EmitterLocation = location;
            Textures = textures;
            Particles = new List<Particle>();
            random = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            timeDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeDuration > 0.15f)
            {
                End = true;
                timeDuration = 0;
            }

            int total = 1;

            for (int i = 0; i < total; i++)
            {
                Particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < Particles.Count; particle++)
            {
                Particles[particle].Update();
                if (Particles[particle].TTL <= 0)
                {
                    Particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public override Particle GenerateNewParticle()
        {
            Texture2D texture = Textures[random.Next(Textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = Vector2.Zero;
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                        0.25f,
                        0.25f,
                        0.25f,
                        (float)random.NextDouble());

            float size = (float)random.NextDouble();
            int ttl = 10;

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

