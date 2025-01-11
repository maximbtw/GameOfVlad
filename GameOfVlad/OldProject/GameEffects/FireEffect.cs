using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.GameEffects
{
    class FireEffect : ParticleEngine
    {
        public float Rotation;
        public float Transparency;
        public int Total = 4;
        public int Life = 30;

        public FireEffect(List<Texture2D> textures, Vector2 location)
                 : base(textures, location)
        {
            EmitterLocation = location;
            Textures = textures;
            Particles = new List<Particle>();
            random = new Random();
        }

        public override void Update(GameTime gameTime)
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

        public override Particle GenerateNewParticle()
        {
            Texture2D texture = Textures[random.Next(Textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() * 2 - 1),
                                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = Rotation;
            float angularVelocity = (float)Math.PI / 720;
            Color color = new Color(
                        0.55f,
                        0.25f,
                        0,
                        Transparency);

            float size = (float)random.NextDouble();
            int ttl = Life/2 + random.Next(Life);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl)
            { Origin = new Vector2(texture.Width / 2, 25), CurrentOrigin = false };
        }
    }
}
