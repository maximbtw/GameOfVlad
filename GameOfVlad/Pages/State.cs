using GameOfVlad.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Pages
{
    public abstract class State
    {
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected Game1 game;
        protected int indexLevel;

        public StateKeyboard StateKeyboard;

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.game = game;
        }

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int indexLevel)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.game = game;
            this.indexLevel = indexLevel;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
