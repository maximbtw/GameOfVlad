using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameOfVlad.Levels;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Sprites.Mobs
{
    class SpaceTrash : Mob
    {
        public enum StateDirection
        {
            Posive,
            Horizontally,
            Upright,
        }

        public StateDirection State = StateDirection.Posive;
        public float Timer = 0;
        public float TurnTime = 7f;

        public SpaceTrash(ContentManager content, Texture2D texture, Vector2 location, Level level)
            : base(content, texture, location, level)
        {
            HPBar = 50;
            Speed = 0.5f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            switch (State)
            {
                case StateDirection.Horizontally:
                    UpdateHorizontally(gameTime);
                    break;
                case StateDirection.Upright:
                    UpdateUpright(gameTime);
                    break;
            }
        }

        private void UpdateHorizontally(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer < TurnTime)
                Location.X += Speed;
            else if (Timer < TurnTime * 2)
                Location.X -= Speed;
            else Timer = 0;
        }

        private void UpdateUpright(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer < TurnTime)
                Location.Y += Speed;
            else if (Timer < TurnTime * 2)
                Location.Y -= Speed;
            else Timer = 0;
        }
    }
}
