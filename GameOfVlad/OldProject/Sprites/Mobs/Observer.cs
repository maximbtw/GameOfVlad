using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Mobs
{
    class Observer : Mob
    {
        private Animation animationLeft;
        private Animation animationRight;
        //Bullet
        private Bullet bullet;
        public float Timer = 0;
        public float TimeToShot = 2.5f;
        public float BulletSpeed = 4f;

        public Observer(ContentManager content, Texture2D texture, Vector2 location, Level level, bool miniBoss = false)
            : base(content, texture, location, level)
        {
            animationRight = new Animation(Content, "Sprite/Observer/Right/", 0.175f, 19);
            animationLeft = new Animation(Content, "Sprite/Observer/Left/", 0.175f, 19);
            Speed = 0.5f;
            HPBar = 45;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            bullet = new Bullet(Content,
                                Content.Load<Texture2D>("Sprite/Bullet/Observer"),
                                Level,
                                this){ 
                                Speed = BulletSpeed,
                                Damage = 10};
            if (miniBoss)
            {
                Color = new Color(0.99f, 0, 0);
                Speed = 0.8f;
                HPBar = 66;
                TimeToShot = 2.1f;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Direction = Level.Player.Location - Location;

            if (Direction != Vector2.Zero)
                Direction.Normalize();
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);

            Location += Direction * Speed;
            UpdateBullet(gameTime);

            if (Math.Abs(Rotation) < 1.5)
            {
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                {
                    Texture = animationLeft.GetTexture;
                }
            }
            else
            {
                animationRight.Update(gameTime);
                if (animationRight.Next)
                {
                    Texture = animationRight.GetTexture;
                }
            }
        }

        private void UpdateBullet(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer > TimeToShot && Location.X > 0 && Location.X < Level.LevelSize.Width
                && Location.Y > 0 && Location.Y < Level.LevelSize.Height)
            {
                Timer = 0;
                var bullet = this.bullet.Clone() as Bullet;
                bullet.Direction = this.Direction;
                bullet.Location = this.Location;
                bullet.Rotation = this.Rotation;
                Level.Shells.Add(bullet);
            }
        }
    }
}
