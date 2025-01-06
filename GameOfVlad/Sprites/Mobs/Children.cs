using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using GameOfVlad.Tools;
using System;
using GameOfVlad.Game.Levels;

namespace GameOfVlad.Sprites.Mobs
{
    class Children : Mob
    {
        Animation animation;

        public Children(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            animation = new Animation(Content, "Sprite/Mother/Children/", 0.2f, 35);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            HPBar = 10;
            Speed = 3;
        }

        public override void Update(GameTime gameTime)
        {
            Direction = Level.Player.Location - Location;
            if (Direction != Vector2.Zero)
                Direction.Normalize();
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);

            animation.Update(gameTime);
            if (animation.Next)
            {
                Texture = animation.GetTexture;
            }

            Location += Direction * Speed;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
