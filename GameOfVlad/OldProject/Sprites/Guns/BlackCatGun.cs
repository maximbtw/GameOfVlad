using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.OldProject.Sprites.Shells;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Guns
{
    class BlackCatGun : Gun
    {
        private Bullet bullet;

        public BlackCatGun(ContentManager content, Mob parent, Level level)
            : base(content, parent, level)
        {
            SkinGun = Content.Load<Texture2D>("Sprite/Rocket/BlackCatGun/Rocket");
            WeaponM = Content.Load<Texture2D>("Sprite/Rocket/BlackCatGun/M");
            WeaponS = Content.Load<Texture2D>("Sprite/Rocket/BlackCatGun/S");

            bullet = new Bullet(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/BlackCatGun/Bullet2"), 
                                level,
                                Parent){
                                Gun = this, 
                                Homing = true,
                                Damage=11,
                                Speed = 7f};
            TimeShot = 4f;
        }

        public override void Shot()
        {
            base.Shot();

            bool shot = true;
            foreach (var shell in Level.Shells)
                if (shell.Gun == this)
                {
                    shot = false;
                    break;
                }
            if (shot)
            {
                var bullet = this.bullet.Clone() as Bullet;
                bullet.Direction = Direction;
                bullet.Location = Parent.Location;
                bullet.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
                Level.Shells.Add(bullet);
            }
        }
    }
}
