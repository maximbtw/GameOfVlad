using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameOfVlad.Levels;
using System;
using GameOfVlad.Sprites.Shells;
using GameOfVlad.Sprites.Mobs;

namespace GameOfVlad.Sprites.Guns
{
    public class MiniSharkGun : Gun
    {
        private Bullet bullet;

        public MiniSharkGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/MiniSharkGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/MiniSharkGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/MiniSharkGun/S");

            bullet = new Bullet(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/MiniSharkGun/Bullet"),
                                Level,
                                Parent){
                                Gun = this,
                                Speed = 10,
                                Damage = 6};
            TimeShot = 0.5f;
        }

        public override void Shot()
        {
            base.Shot();

            var bullet = this.bullet.Clone() as Bullet;
            bullet.Location = Parent.Location;
            bullet.Direction = Direction;
            bullet.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            Level.Shells.Add(bullet);
        }
    }
}
