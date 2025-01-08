using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using GameOfVlad.Sprites.Mobs;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;

namespace GameOfVlad.Sprites
{
    public class Wall : ColldesSprite
    {
       public Rectangle RectangleWall
        {
            get
            {
                return new Rectangle((int)Location.X + 38, (int)Location.Y + 40, Texture.Width + 2, Texture.Height + 3);
            }
        }

        public Wall(ContentManager content, Texture2D texture, Vector2 location, Level level)
                            : base(content, texture, location, level)
        {

        }

        public override void WasShot(int damage)
        {
            
        }
    }
}
