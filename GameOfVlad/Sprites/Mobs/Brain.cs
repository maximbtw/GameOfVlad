using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using GameOfVlad.Game.Levels;
using GameOfVlad.Utils;

namespace GameOfVlad.Sprites.Mobs
{
    class Brain : Mob
    {
        public enum State { left,right}
        public State StateRotation = State.left;
        private Animation animationLeft;
        private Animation animationRight;

        public float Distanse = 400;
        private Vector2 positionSwich;
        private bool hasJumped = false;

        public Brain(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            animationLeft = new Animation(Content, "Sprite/Brain/Left/", 7, 8);
            animationRight = new Animation(Content, "Sprite/Brain/Right/", 7, 8);
            HPBar = 5;
            Speed = 0.5f;
            Swich();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Math.Abs(positionSwich.X - Location.X) > Distanse)
                Swich();
            if (StateRotation == State.left)
            {
                Velocity.X = -Speed;
                animationLeft.Update(gameTime);
                if (animationLeft.Next)
                {
                    Texture = animationLeft.GetTexture;
                }
            }
            else
            {
                Velocity.X = Speed;
                animationRight.Update(gameTime);
                if (animationRight.Next)
                {
                    Texture = animationRight.GetTexture;
                }
            }
            if (hasJumped)
            {
                float i = 1;
                Velocity.Y += 0.15f * i;
            }
            if (!hasJumped)
                Velocity.Y = 0;

            int count = 0;
            foreach (var wall in Level.Walls)
            {
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(wall)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(wall)))
                    this.Velocity.X = 0;
                if (this.Velocity.Y < 0 & this.IsTouchingBottom(wall))
                    this.Velocity.Y = 0;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(wall))
                {
                    this.Velocity.Y = 0;
                    hasJumped = false;
                    count++;
                }
                else if (count == 0) hasJumped = true;
            }
            Location += Velocity;
        }

        private void Swich()
        {
            StateRotation = (StateRotation == State.left) ? State.right : State.left;
            positionSwich = Location;
        }
    }
}
