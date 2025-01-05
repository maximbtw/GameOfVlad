    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework;
    using GameOfVlad.Levels;
    using System;

namespace GameOfVlad.Sprites.Bonuses
{
    public class HealthBox : Bonus
    {
        public int BonusHP;

        public HealthBox(ContentManager content, Texture2D texture, Vector2 location, Level level)
                    : base(content, texture, location, level)
        {
            BonusHP = 10;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Location.Y > Level.LevelSize.Height - 50) 
                Speed = 0;
            else
                Rotation += (float)Math.PI / 65;
        }

        public override void ActivateBonus()
        {
            if (Level.Player.HPBar + BonusHP > 100)
                Level.Player.HPBar = 100;
            else
                Level.Player.HPBar += BonusHP;
        }
    }
}
