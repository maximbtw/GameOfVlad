using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using GameOfVlad.Sprites.Shells;
using Microsoft.Xna.Framework;
using GameOfVlad.GameEffects;
using GameOfVlad.Tools;
using System;
using GameOfVlad.Game.Levels;

namespace GameOfVlad.Sprites.Mobs
{
    public class Phantom : Mob
    {
        public float Timer = 0;
        public float TimeToShot = 4f;
        public float BulletSpeed = 4f;
        public float TimeTeleport = 0;
        public float TimeToTeleport = 5f;

        private PhantomBullet bullet;
        private Animation animation;
        private Random random;

        public Phantom(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            HPBar = 10;
            bullet = new PhantomBullet(Content, 
                                       Content.Load<Texture2D>("Sprite/Phantom/Shell1"), 
                                       Level, 
                                       this);
            animation = new Animation(Content, "Sprite/Phantom/", 10, 28);
            random = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            TimeTeleport += (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateBullet(gameTime);

            if (TimeTeleport > TimeToTeleport)
            {
                Teleport();
            }

            animation.Update(gameTime);
            if (animation.Next)
            {
                Texture = animation.GetTexture;
            }
        }

        public override void WasShot(int damage)
        {
            base.WasShot(damage);
            Teleport();
        }

        private void Teleport()
        {
            TimeTeleport = 0;
            Location = new Vector2(
                random.Next(150, (int)Level.LevelSize.Width - 150),
                random.Next(150, (int)Level.LevelSize.Height - 150));

            if ((Location - Level.Player.Location).Length() < 300)
                Teleport();
        }

        private void UpdateBullet(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer > TimeToShot)
            {
                Timer = 0;

                Vector2 direction;
                direction.X = Level.Player.Location.X - Location.X;
                direction.Y = Level.Player.Location.Y - Location.Y;
                if (direction != Vector2.Zero)
                    direction.Normalize();
                var rotation = (float)Math.Atan2(direction.Y, direction.X);

                var bullet = this.bullet.Clone() as PhantomBullet;
                bullet.Direction = direction;
                bullet.Location = new Vector2(Location.X + 25,Location.Y + 125);
                bullet.Rotation = rotation;
                bullet.Speed = BulletSpeed;
                Level.Shells.Add(bullet);
            }
        }

        public class PhantomBullet : GhostBullet
        {
            private ParticleEngine effect;
            private Animation animation;

            public PhantomBullet(ContentManager content, Texture2D texture, Level level, Mob parent)
               : base(content, texture, level, parent)
            {
                animation = new Animation(Content, "Sprite/Phantom/Shell", 7, 4);

                List<Texture2D> texturesEffect = new List<Texture2D>();
                texturesEffect.Add(Content.Load<Texture2D>("Sprite/Phantom/Effect1"));
                texturesEffect.Add(Content.Load<Texture2D>("Sprite/Phantom/Effect2"));
                effect = new ParticleEngine(texturesEffect, 3, 10) 
                {
                    R = 0.5f,
                    G = 0,
                    B = 0.5f,
                };

                HPBar = 5;
                Damage = 2;
            }

            public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                base.Draw(gameTime, spriteBatch);
                effect.Draw(spriteBatch);
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                animation.Update(gameTime);
                if (animation.Next)
                    Texture = animation.GetTexture;
                effect.EmitterLocation = Location;
                effect.Update(gameTime);
            }
        }
    }
}
