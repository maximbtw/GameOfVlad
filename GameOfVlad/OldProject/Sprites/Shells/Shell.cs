using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.Sprites.Guns;
using GameOfVlad.OldProject.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Shells
{
    public abstract class Shell : ColldesSprite
    {
        public Gun Gun;
        public Bullet ParticleBullet;
        public int Damage;
        public bool Homing = false;

        public Shell(ContentManager content, Texture2D texture, Level level, Mob parent)
                : base(content, texture, level, parent)
        {

        }

        public Shell(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {

        }

        public abstract void Create(Vector2 direction);

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
