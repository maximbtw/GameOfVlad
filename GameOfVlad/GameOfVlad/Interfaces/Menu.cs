using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Levels;
using GameOfVlad.Tools;
using GameOfVlad.Pages;

namespace GameOfVlad.Interfaces
{
    public class Menu
    {
        protected Game1 game;
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected Level level;
        protected MainMenu mainMenu;

        public Texture2D Texture;
        public Vector2 Location;

        public List<Button> Buttons;

        public Menu(Game1 game, ContentManager content, GraphicsDevice graphicsDevice,MainMenu mainMenu)
        {
            this.game = game;
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.mainMenu = mainMenu;
        }

        public Menu(Game1 game, ContentManager content, GraphicsDevice graphicsDevice, Level level)
        {
            this.game = game;
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.level = level;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
            foreach (var button in Buttons)
                button.Draw(gameTime, spriteBatch);
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (var button in Buttons)
                button.Update(gameTime);
        }


        public void ToContinue()
        {
            level.StateProcess = Level.State.Play;
        }

        public void RestartLevel(int indexLevel)
        {
            game.ChangeState(new GameLevels(game, graphicsDevice, content, indexLevel));
        }

        public void ToMainMenu()
        {
            game.ChangeState(new MainMenu(game, graphicsDevice, content));
            game.NextMusic = game.BackgraundMusic;
        }

        public void ToLevelMap()
        {
            game.ChangeState(new MapLevels(game, graphicsDevice, content));
            game.NextMusic = game.BackgraundMusic;
        }

        public void ToNextLevel(int indexLevel)
        {
            game.ChangeState(new GameLevels(game, graphicsDevice, content, indexLevel + 1));
        }
    }
}
