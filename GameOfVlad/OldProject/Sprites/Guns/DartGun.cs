using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Guns
{
    class DartGun : Gun
    {
        private DartBullet bullet;

        public DartGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/DartGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/DartGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/DartGun/S");

            bullet = new DartBullet(Content,
                                    Content.Load<Texture2D>("Sprite/Rocket/DartGun/Bullet"),                                  
                                    Level,
                                    Parent) {
                                    Gun = this,};
            TimeShot = 0.75f;
        }

        public override void Shot()
        {
            base.Shot();

            var bullet = this.bullet.Clone() as DartBullet;
            bullet.Location = Parent.Location;
            bullet.Direction = Direction;
            bullet.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            Level.Shells.Add(bullet);
        }

        public class DartBullet : Bullet
        {
            public DartBullet(ContentManager content, Texture2D texture, Level level, Mob parent)
                     : base(content, texture, level, parent)
            {
                Speed = 6;
                Damage = 8;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                Direction.Y += 0.005f;
                Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            }
        }
    }
}
