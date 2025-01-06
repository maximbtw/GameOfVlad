using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameOfVlad.Sprites.Mobs;

namespace GameOfVlad.Sprites.Guns
{
    public class Gun
    {
        public Mob Parent;
        public Level Level;
        public ContentManager Content;
        public Texture2D SkinGun;
        public Texture2D WeaponM;
        public Texture2D WeaponS;

        protected MouseState mouse;
        protected float Timer;
        protected bool ShotShell;
        protected float TimeShot;
        protected Vector2 Direction;

        public Gun(ContentManager content, Mob parent, Level level)
        {
            Content = content;
            Parent = parent;
            Level = level;
            ShotShell = false;
            Timer = 20f;
        }

        public virtual void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed && !ShotShell && Timer > TimeShot)
                ShotShell = true;
            if (ShotShell)
                Shot();
        }

        public virtual void Shot()
        {
            Timer = 0;
            ShotShell = false;
            
            Direction.X = mouse.X - Parent.Location.X;
            Direction.Y = mouse.Y - Parent.Location.Y;
            if (Direction != Vector2.Zero)
                Direction.Normalize();
        }
    }
}
