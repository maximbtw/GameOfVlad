using System;
using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Sprites.Bonuses
{
    public class Bonus : ColldesSprite
    {
        Random random;

        public Bonus(ContentManager content, Texture2D texture, Vector2 location, Level level)
                    : base(content, texture, location, level)
        {
            random = new Random();

            Speed = 3f;
            Direction = new Vector2(0, 1);
            Location = new Vector2(random.Next(50, (int)Level.LevelSize.Width - 75), -100);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Location += Direction * Speed;
        }

        public virtual void ActivateBonus()
        {
        }
    }
}
