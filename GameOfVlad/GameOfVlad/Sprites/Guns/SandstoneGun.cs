using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameOfVlad.Sprites.Shells;
using Microsoft.Xna.Framework;
using GameOfVlad.Levels;
using System;
using GameOfVlad.Sprites.Mobs;

namespace GameOfVlad.Sprites.Guns
{
    public class SandstoneGun : Gun
    {
        private SandBullet bullet;
        private Random random;

        public SandstoneGun(ContentManager content, Mob parent, Level level) 
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/SandstoneGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/SandstoneGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/SandstoneGun/S");

            bullet = new SandBullet(Content,
                                    Content.Load<Texture2D>("Sprite/Rocket/SandstoneGun/Bullet"),
                                    Level,
                                    Parent){ 
                                    Gun = this};
            TimeShot = 1f;
            random = new Random();
        }

        public override void Shot()
        {
            base.Shot();

            var n = random.Next(8, 13);
            var direction = Direction;
            var rotat = 0.04f;
            var speed = 12f;

            for (int i = 0; i < n; i++)
            {
                var bullet = this.bullet.Clone() as SandBullet;
                bullet.Speed = speed;
                bullet.Direction = Direction;
                bullet.StartPosition = Parent.Location;
                bullet.Location = Parent.Location;
                bullet.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
                Level.Shells.Add(bullet);

                GetDirection(direction, rotat);
                rotat += 0.04f;
                speed -= 0.25f;
            }
        }

        private void GetDirection(Vector2 direction, float y)
        {
            var pov = Vector2.Zero;
            if (direction.Y < 0.5f && direction.Y > 0)
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

        public class SandBullet : Bullet
        {
            public Vector2 StartPosition;
            int c;

            public SandBullet(ContentManager content, Texture2D texture, Level level, Mob parent)
                   : base(content, texture, level, parent)
            {
                Damage = 1;
                Speed = 12f;
                c = 255;
            }

            public override void Update(GameTime gameTime)
            {
                var distance = (StartPosition - Location).Length();
                if (HPBar < 1 || distance > 400)
                    Dead = true;
                Location += Speed * Direction;
                Color = new Color(c, c, c, 0);
                c -= 5;
            }
        }
    }
}
