using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.GameObjects.UI.Components.ButtonComponent;
using GameOfVlad.Scenes.MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.OldProject.Interfaces
{
    public class Menu
    {
        protected GameOfVlad game;
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected Level level;
        protected MainMenuScene mainMenu;

        public Texture2D Texture;
        public Vector2 Location;

        public List<Button> Buttons;

        public Menu(GameOfVlad game, ContentManager content, GraphicsDevice graphicsDevice,MainMenuScene mainMenu)
        {
            this.game = game;
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.mainMenu = mainMenu;
        }

        public Menu(GameOfVlad game, ContentManager content, GraphicsDevice graphicsDevice, Level level)
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
         //   game.ChangeState(new GameLevels(game, graphicsDevice, content, indexLevel));
        }

        public void ToMainMenu()
        {
            //game.ChangeState(new MainMenuPage(game, graphicsDevice, content));
            //  game.NextMusic = game.BackgraundMusic;
        }

        public void ToLevelMap()
        {
            //game.ChangeState(new MapPage(game, graphicsDevice, content));
         //   game.NextMusic = game.BackgraundMusic;
        }

        public void ToNextLevel(int indexLevel)
        {
           // game.ChangeState(new GameLevels(game, graphicsDevice, content, indexLevel + 1));
        }
    }
}
