using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Mobs
{
    class Butler : Mob
    {
        private Animation animationLeft;
        private Animation animationRight;

        private Bullet bullet;
        private Random random;

        public float TimeToShot = 3;
        public float Time = 0;
        public float BulletSpeed = 7.5f;

        public Butler(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            random = new Random();
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            animationRight = new Animation(Content, "Sprite/Butler/Right/", 0.20f, 8);
            animationLeft = new Animation(Content, "Sprite/Butler/Left/", 0.20f, 8);

            HPBar = 70;
            Speed = 1;

            bullet = new Bullet(Content,
                                Content.Load<Texture2D>("Sprite/Butler/BulletSize3"),
                                Level,
                                this){
                                Speed = BulletSpeed,
                                Damage = 8 };
        }

        public override void Update(GameTime gameTime)
        {
            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Time > TimeToShot && Location.X > 0 && Location.X < Level.LevelSize.Width
                && Location.Y > 0 && Location.Y < Level.LevelSize.Height)
            {
                Time = 0;
                Shot();
            }

            Direction = Level.Player.Location - Location;
            if (Direction != Vector2.Zero)
                Direction.Normalize();
            Rotation = (float)Math.Atan2(-Direction.Y, -Direction.X);

            Location += Direction * Speed;

            if (Math.Abs(Rotation) > 1.5)
            {
                animationRight.Update(gameTime);
                if (animationRight.Next)
                    Texture = animationRight.GetTexture;
            }
            else
            {
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                    Texture = animationLeft.GetTexture;
            }
        }

        private void Shot()
        {
            var n = random.Next(4, 6);
            var direction = Direction;

            for (int i = 0; i < n; i++)
            {
                var bullet = this.bullet.Clone() as Bullet;
                bullet.Direction = Direction;
                bullet.Location = Location;
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
    }
}
