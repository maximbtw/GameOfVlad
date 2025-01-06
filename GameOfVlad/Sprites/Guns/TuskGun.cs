using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.Sprites.Shells;
using GameOfVlad.Sprites.Mobs;

namespace GameOfVlad.Sprites.Guns
{
    class TuskGun : Gun
    {
        private Bullet bullet;
        private List<Texture2D> texturesBullet;
        private Random random;

        public TuskGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            random = new Random();
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/TuskGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/TuskGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/TuskGun/S");

            texturesBullet = new List<Texture2D>
            {
                Content.Load<Texture2D>("Sprite/Rocket/TuskGun/Bullet1"),
                Content.Load<Texture2D>("Sprite/Rocket/TuskGun/Bullet2")
            };

            bullet = new Bullet(Content,
                                texturesBullet[0],
                                Level,
                                Parent){
                                Gun = this,
                                Speed = 7.5f,
                                Damage = 2};
            TimeShot = 1.5f;
        }

        public override void Shot()
        {
            base.Shot();
            var n = random.Next(4, 6);
            var direction = Direction;

            for (int i = 0; i < n; i++)
            {
                var bullet = this.bullet.Clone() as Bullet;
                bullet.Texture = texturesBullet[random.Next(texturesBullet.Count)];
                bullet.Direction = Direction;
                bullet.Location = Parent.Location;
                bullet.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
                Level.Shells.Add(bullet);

                if (i == 0)
                {
                    GetDirection(direction, 0.1f);
                }
                if (i == 1)
                {
                    GetDirection(direction, 0.2f);
                }
                if (i == 2)
                {
                    GetDirection(direction, -0.1f);
                }
                if (i == 3)
                {
                    GetDirection(direction, -0.2f);
                }
            }
        }

        private void GetDirection(Vector2 direction, float y)
        {
            var pov = Vector2.Zero;
            if(direction.Y < 0.5f && direction.Y > 0)
            {
                pov = new Vector2(0, -y);
            }
            else if (direction.Y > -0.5f && direction.Y < 0)
            {
                pov = new Vector2(0, y);
            }
            else if (direction.X > 0)
            {
                pov = new Vector2(-y, 0);
            }
            else if (direction.X < 0)
            {
                pov = new Vector2(y, 0);
            }

            direction += pov;
            Direction = direction;
            if (Direction != Vector2.Zero)
                Direction.Normalize();
        }
    }
}
