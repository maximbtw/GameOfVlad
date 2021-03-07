using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameEffects
{
    public class ParticleEngine
    {
        protected Random random;
        public Vector2 EmitterLocation { get; set; }
        protected List<Particle> Particles;
        protected List<Texture2D> Textures;

        public int Total;
        public int Life;
        public float R = -1;
        public float G = -1;
        public float B = -1;
        public float Alpha = -1;

        public float Rotation = 0;
        public float AngularVelocity = -1;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.Textures = textures;
            this.Particles = new List<Particle>();
            random = new Random();
        }

        public ParticleEngine(List<Texture2D> textures,int total,int life)
        {
            Total = total;
            Life = life;
            this.Textures = textures;
            this.Particles = new List<Particle>();
            random = new Random();
        }

        public virtual void Update(GameTime gameTime)
        {
            int total = Total;

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

        public virtual Particle GenerateNewParticle()
        {
            Texture2D texture = Textures[random.Next(Textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() * 2 - 1),
                                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = Rotation;
            float angularVelocity = (AngularVelocity==-1) ? (0.1f * (float)(random.NextDouble() * 2 - 1)) : AngularVelocity;
            Color color = new Color(
                        (R == -1) ? (float)random.NextDouble() : R,
                        (G == -1) ? (float)random.NextDouble() : G,
                        (B == -1) ? (float)random.NextDouble() : B,
                        (Alpha == -1) ? (float)random.NextDouble() : Alpha);

            float size = (float)random.NextDouble();
            int ttl = Life + random.Next(Life*2);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < Particles.Count; index++)
            {
                Particles[index].Draw(spriteBatch);
            }
        }

        public void ClearEffect()
        {
            Particles.Clear();
        }
    }
}
