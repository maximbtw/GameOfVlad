using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.GameEffects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Mobs
{
    class Meteorit : Mob
    {
        private ParticleEngine effect;

        public int MinHp = 5;
        public int MaxHp = 15;

        public int MaxSpeed = 20;
        public Meteorit(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            List<Texture2D> textures = new List<Texture2D> {
                Content.Load<Texture2D>("Sprite/Meteorit/ParticleEffect1"),
                Content.Load<Texture2D>("Sprite/Meteorit/ParticleEffect2"),
                Content.Load<Texture2D>("Sprite/Meteorit/ParticleEffect3"),
             };
            effect = new ParticleEngine(textures, 3, 10) 
            {
                R = 0.55f,
                G = 0.5f,
                B = 0.5f
            };
            Spawn();
            Respawn();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
            effect.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            effect.EmitterLocation = new Vector2(Location.X+15,Location.Y+15);
            effect.Update(gameTime);

            Location += Direction;
            if (Location.Y > Level.LevelSize.Height + 300)
                Respawn();
        }

        private void Spawn()
        {
            HPBar = RandomV.GetRandom1(MinHp, MaxHp);
        }

        private void Respawn()
        {
            Location = new Vector2(RandomV.GetRandom1(0, (int)Level.LevelSize.Width), -500);
            Direction = new Vector2(RandomV.GetRandom1(-1, 1), RandomV.GetRandom1(2, MaxSpeed));
        }

        public override void WasShot(int damage)
        {
            HPBar-=damage;
            if (HPBar < 1)
            {
                if (1 == RandomV.GetRandom1(1, 4))
                    Dead = true;
                else
                {
                    Spawn();
                    Respawn();
                }
            }
        }
    }
}
