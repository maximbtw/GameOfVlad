using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Mobs
{
    public class EX40K : Mob
    {
        private Animation animation;
        private Bullet bullet;

        public float Timer = 0;
        public float TurnTime = 7f;
        public float TimeToShot = 7.5f;
        public float BulletSpeed = 4.5f;

        public EX40K(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            animation = new Animation(Content, "Sprite/EX40K/", 10, 30);

            var textures = new List<Texture2D>
                {
                    Content.Load<Texture2D>("Sprite/Phantom/Effect2"),
                    Content.Load<Texture2D>("Sprite/Meteorit/ParticleEffect1"),
                };
            var effectBullet = new ParticleEngine(textures, 3, 6){ R = 0.75f, G = 0.5f, B = 0 };
            bullet = new Bullet(Content,
                                Content.Load<Texture2D>("Sprite/EX40K/Bullet"),
                                Level,
                                this){
                                Homing = true,
                                Damage = 3,
                                Speed = BulletSpeed,
                                Effect = effectBullet};
            HPBar = 12;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateBullet(gameTime);
            animation.Update(gameTime);
            if (animation.Next)
            {
                Texture = animation.GetTexture;
            }
        }

        private void UpdateBullet(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool shot = true;
            foreach(var shell in Level.Shells)
                if (shell.Parent == this)
                {
                    shot = false;
                    break;
                }


            if (Timer > TimeToShot && shot)
            {
                Timer = 0;
                CreateBullet(new Vector2(Location.X + 10, Location.Y + 30));
                CreateBullet(new Vector2(Location.X + Size.Width, Location.Y + 30));
            }
        }

        private void CreateBullet(Vector2 location)
        {
            var bullet = this.bullet.Clone() as Bullet;
            bullet.Location = location;
            Level.Shells.Add(bullet);
        }
    }
}
