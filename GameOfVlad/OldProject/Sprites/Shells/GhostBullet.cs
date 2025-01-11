using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Mobs;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Shells
{
    public class GhostBullet : Bullet
    {
        public GhostBullet(ContentManager content, Texture2D texture, Level level, Mob parent)
               : base(content, texture, level, parent)
        {
            HPBar = 5;
            Damage = 2;
        }

        public override bool WasHit(ColldesSprite mob)
        {
            foreach (var shell in mob.GhostBulletHit)
                if (shell == this)
                    return false;

            if (this.Parent != mob && Collides.Collide(this, mob))
            {
                mob.GhostBulletHit.Add(this);
                mob.WasShot(Damage);
                return true;
            }
            return false;
        }
    }
}
