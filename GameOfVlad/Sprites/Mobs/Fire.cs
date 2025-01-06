using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameOfVlad.Tools;
using System;
using GameOfVlad.GameEffects;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;

namespace GameOfVlad.Sprites.Mobs
{
    class Fire : Mob
    {
        public enum Position
        {
            Top = 1,
            Right,
            Down,
            Left
        }

        private List<FireEffect> FireParticleEngine;

        public float GlobalTime = 0;
        public float TimeRespawn = 6f;
        private float a = 0;
        private bool rotat = true;

        public Position PositionOnLevel;

        public Fire(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            Content = content;

            HPBar = 10000;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Size = new Size(texture.Width, texture.Height);
            Speed = 5f;

            List<Texture2D> texturesFire = new List<Texture2D>();
            texturesFire.Add(Content.Load<Texture2D>("Sprite/Fire/FireEffect4"));
            texturesFire.Add(Content.Load<Texture2D>("Sprite/Fire/FireEffect5"));
            texturesFire.Add(Content.Load<Texture2D>("Sprite/Fire/FireEffect6"));
            texturesFire.Add(Content.Load<Texture2D>("Sprite/Fire/FireEffect7"));

            List<Texture2D> texturesSmokeandFire = new List<Texture2D>();
            texturesSmokeandFire.Add(Content.Load<Texture2D>("Sprite/Fire/SmokeEffect2"));
            texturesSmokeandFire.Add(Content.Load<Texture2D>("Sprite/Fire/FireEffect6"));
            texturesSmokeandFire.Add(Content.Load<Texture2D>("Sprite/Fire/SmokeEffect1"));

            FireParticleEngine = new List<FireEffect>
            {
                    new FireEffect(texturesFire, Vector2.Zero){Transparency = 0.35f },
                    new FireEffect(texturesFire, Vector2.Zero){Transparency = 0.5f },
                    new FireEffect(texturesSmokeandFire, Vector2.Zero){Transparency = 0.65f ,Total=3,Life = 26 },
                    new FireEffect(texturesSmokeandFire, Vector2.Zero){Transparency = 0.8f,Total=2,Life = 20 },
                    new FireEffect(texturesSmokeandFire, Vector2.Zero){Transparency = 0.85f ,Total=2,Life = 20},
                    new FireEffect(texturesSmokeandFire, Vector2.Zero){Transparency = 0.95f ,Total=2,Life = 20},
            };
            Respawn();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            foreach (var effect in FireParticleEngine)
                effect.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            GlobalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GlobalTime > 1f && GlobalTime < 2f)
            {
                a = 0.5f;
            }
            if (GlobalTime > 2f && GlobalTime < 2.5f)
            {
                a = 2.5f;
            }
            if (GlobalTime > 2.5f && GlobalTime < 4f)
            {
                a = 0;
            }
            if (GlobalTime > 4f && GlobalTime < TimeRespawn)
            {
                a = 1;
                if (rotat)
                {
                    Direction *= -1;
                    rotat = false;
                }
            }
            if (GlobalTime > TimeRespawn)
            {
                a = 0;
                GlobalTime = 0;
                rotat = true;
                Respawn();
            }
            Location += Direction * a;

            UpdateEffects(gameTime);
        }

        public override bool WasHit(ColldesSprite mob)
        {
            mob.WasShot(1);
            return true;
        }

        private void UpdateEffects(GameTime gameTime)
        {
            var n = 300;
            foreach (var effect in FireParticleEngine)
            {
                switch (PositionOnLevel)
                {
                    case Position.Top:
                        effect.EmitterLocation = new Vector2(Location.X, Location.Y + n);
                        break;

                    case Position.Right:
                        effect.EmitterLocation = new Vector2(Location.X - n, Location.Y);
                        break;

                    case Position.Down:
                        effect.EmitterLocation = new Vector2(Location.X, Location.Y - n);
                        break;

                    case Position.Left:
                        effect.EmitterLocation = new Vector2(Location.X + n, Location.Y);
                        break;
                }
                effect.Rotation = Rotation - (float)Math.PI / 180;
                effect.Update(gameTime);
                n -= 100;
            }
        }

        private void Respawn()
        {
            PositionOnLevel = (Position)RandomV.GetRandom5(1, 5);
            switch (PositionOnLevel)
            {
                case Position.Top:
                    Location = new Vector2(RandomV.GetRandom5(0, (int)Level.LevelSize.Width), -375);
                    Rotation = 0;
                    Direction = new Vector2(0, Speed);
                    break;

                case Position.Right:
                    Location = new Vector2(Level.LevelSize.Width+375, RandomV.GetRandom5(0, (int)Level.LevelSize.Height));
                    Rotation = (float)Math.PI / 2;
                    Direction = new Vector2(-Speed, 0);
                    break;

                case Position.Down:
                    Location = new Vector2(RandomV.GetRandom5(0, (int)Level.LevelSize.Width), Level.LevelSize.Height+375);
                    Rotation = (float)Math.PI;
                    Direction = new Vector2(0, -Speed);
                    break;

                case Position.Left:
                    Location = new Vector2(-375, RandomV.GetRandom5(0, (int)Level.LevelSize.Height));
                    Rotation = (float)(3 * Math.PI / 2);
                    Direction = new Vector2(Speed, 0);
                    break;
            }
        }
    }
}
