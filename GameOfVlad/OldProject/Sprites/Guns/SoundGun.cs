using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Guns
{
    public class SoundGun : Gun
    {
        private List<SoundBullet> bullets;

        private Vector2 prevLocation;
        private int index = 0;
        private int TTK = 15;

        int ttk = 15;

        public SoundGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/SoundGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/SoundGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/SoundGun/S");

            bullets = new List<SoundBullet>
            {
                new SoundBullet(Content, Content.Load<Texture2D>("Sprite/Rocket/SoundGun/Bullet1"), Level, Parent){ Gun = this },
                new SoundBullet(Content, Content.Load<Texture2D>("Sprite/Rocket/SoundGun/Bullet2"), Level, Parent){ Gun = this },
                new SoundBullet(Content, Content.Load<Texture2D>("Sprite/Rocket/SoundGun/Bullet3"), Level, Parent){ Gun = this },
                new SoundBullet(Content, Content.Load<Texture2D>("Sprite/Rocket/SoundGun/Bullet4"), Level, Parent){ Gun = this },
            };

            TimeShot = 1.5f;
        }

        public override void Shot()
        {
            if (index == 0)
            {
                Direction.X = mouse.X - Parent.Location.X;
                Direction.Y = mouse.Y - Parent.Location.Y;
                if (Direction != Vector2.Zero)
                    Direction.Normalize();
            }

            if (TTK % ttk == 0)
            {
                var bullet = this.bullets[index].Clone() as SoundBullet;

                if (index == 0)
                {
                    bullet.StartPosition = bullet.Location = prevLocation = Parent.Location;
                }
                else
                {
                    bullet.StartPosition = bullet.Location = prevLocation;
                }

                bullet.Damage = 2;
                bullet.Speed = 7.5f;
                bullet.Direction = Direction;
                bullet.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
                Level.Shells.Add(bullet);
                ttk--;

                if (index == 3)
                {
                    index = 0;
                    ShotShell = false;
                    Timer = 0;
                    ttk = TTK = 15;
                }
                else
                    index++;
            }
            TTK++;
        }

        public class SoundBullet : GhostBullet
        {
            public Vector2 StartPosition;
            int c;

            public SoundBullet(ContentManager content, Texture2D texture, Level level, Mob parent)
               : base(content, texture, level, parent)
            {
                HPBar = 5;
                Damage = 2;
                c = 255;
            }

            public override void Update(GameTime gameTime)
            {
                var distance = (StartPosition - Location).Length();
                if (HPBar < 1 || distance > 800)
                    Dead = true;
                Location += Speed * Direction;

                Color = new Color(c, c, c, 0);
                if (distance > 150)
                {
                    c -= 3;
                }
            }
        }
    }
}
