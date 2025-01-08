using GameOfVlad.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.Utils;

namespace GameOfVlad.Sprites.Shells
{
    public class Lazer : Shell
    {
        protected float Time;
        protected List<Bullet> bullets;

        private bool addBullet = true;
        public bool GhostLazer = true;
        public Params Param;

        public Lazer(ContentManager content, Texture2D texture, Level level, Mob parent)
                     :base(content, texture, level, parent)
        {
            ParticleBullet = new Bullet(Content, Texture, Level, Parent);
            bullets = new List<Bullet>();
            Damage= 5;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var bullet in bullets)
            {
                bullet.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (bullets.Count > 0)
            {
                Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Time < 0.25f)
                {
                    foreach (var bullet in bullets)
                    {
                        bullet.Update(gameTime);
                    }
                }
                else
                {
                    bullets.Clear();
                    Time = 0;
                    Dead = true;
                }
            }
        }

        public override void Create(Vector2 direction)
        {
            for (int i = 0; i < 200; i++)
            {
                if (addBullet)
                {
                    var bullet = ParticleBullet.Clone() as Bullet;
                    bullet.Speed = 0;
                    bullet.Direction = direction;
                    bullet.Location = Parent.Location + direction * Param.Tick;
                    bullet.Rotation = (float)Math.Atan2(direction.Y, direction.X);
                    bullet.Color = new Color(Param.R, Param.G, Param.B);
                    bullets.Add(bullet);
                    Param.Update();
                    if (!GhostLazer)
                    {
                        foreach (var mob in Level.HostileMobs)
                        {
                            if (bullet.WasHit(mob))
                            {
                                addBullet = false;
                            }
                        }
                    }
                }
                else
                {
                    addBullet = true;
                    break;
                }
            }
        }

        public override bool WasHit(ColldesSprite mob) 
        {
            foreach (var shell in mob.LazerHit)
                if (shell == this)
                    return false;

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Parent != mob && Collides.Collide(bullets[i], mob))
                {
                    mob.LazerHit.Add(this);
                    mob.WasShot(Damage);
                    return true;
                }
            }
            return false;
        }

        public struct Params
        {
            public int Tick;
            private int tick;
            public float R;
            private float r;
            public float G;
            private float g;
            public float B;
            private float b;

            public Params(int tick, float R, float r, float G, float g, float B, float b)
            {
                Tick = 0;
                this.tick = tick;
                this.R = R;
                this.r = r;
                this.G = G;
                this.g = g;
                this.B = B;
                this.b = b;
            }

            public void Update()
            {
                Tick += tick;
                R -= r;
                G -= g;
                B -= b;
            }
        }
    }
}
