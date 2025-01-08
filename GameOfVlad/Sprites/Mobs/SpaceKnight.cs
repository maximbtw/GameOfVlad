using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.Utils;

namespace GameOfVlad.Sprites.Mobs
{
    public class SpaceKnight : Mob
    {
        private Animation animationLeft;
        private Animation animationRight;


        public SpaceKnight(ContentManager content, Texture2D texture, Vector2 location, Level level, bool miniBoss = false)
            : base(content, texture, location, level)
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            animationLeft = new Animation(Content, "Sprite/SpaceKnight/Left/", 0.3f, 8);
            animationRight = new Animation(Content, "Sprite/SpaceKnight/Right/", 0.3f, 8);
            HPBar = 40;
            Speed = 2f;
            if (miniBoss)
            {
                Color = new Color(0.99f, 0, 0);
                HPBar = 70;
                Speed = 3.1f;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Direction = Level.Player.Location - Location;
            if (Direction != Vector2.Zero)
                Direction.Normalize();
            Rotation = (float)Math.Atan2(-Direction.Y, -Direction.X);
            if (Math.Abs(Rotation) > 1.5)
            {
                animationRight.Update(gameTime);
                if (animationRight.Next)
                    Texture = animationRight.GetTexture;
            }
            else
            {
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                    Texture = animationLeft.GetTexture;
            }

            Location += Direction * Speed;
        }
    }
}
