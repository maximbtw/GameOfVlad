using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using GameOfVlad.Tools;
using GameOfVlad.Interfaces;

namespace GameOfVlad.Pages
{
    public class MainMenu : State
    {
        public enum State
        {
            Noraml,
            Gallery,
            Setting
        }

        public State _State;
        public Gallery Gallery { get; set; }

        private List<Button> buttons;
        private Texture2D backgraund;

        public MainMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            StateKeyboard = new StateKeyboard();
            backgraund = content.Load<Texture2D>("Pages/MainMenu/Backgraund");
            _State = State.Noraml;
            Gallery = new Gallery(game, content, graphicsDevice, this);

            buttons = new List<Button>
            {
                new Button(content, new Vector2(1420, 150),"Buttons/Start")
                        { Action = () =>{
                            game.ChangeState(new GameLevels(game, graphicsDevice,content,1));
                        } },
                new Button(content, new Vector2(1410, 300), "Buttons/Levels")
                        { Action = ()=>{
                            game.ChangeState(new MapLevels(game, graphicsDevice,content));
                        } },
                new Button(content, new Vector2(1320, 450), "Buttons/MiniGames"),
                new Button(content, new Vector2(1410, 600), "Buttons/Gallery"){ Action = () =>_State = State.Gallery},
                new Button(content, new Vector2(1320, 750), "Buttons/Setting"),
                new Button(content, new Vector2(1420, 900), "Buttons/Exit"){ Action = () => game.Exit() },
            };
            Mouse.SetCursor(MouseCursor.Arrow);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgraund, new Rectangle(0, 0, 1920, 1080), Color.White);

            switch (_State)
            {
                case State.Noraml:
                    DrawMainMenu(gameTime, spriteBatch);
                    break;
                case State.Gallery:
                    Gallery.Draw(gameTime, spriteBatch);
                    break;
                case State.Setting:
                    break;
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            StateKeyboard.UpdateStart();

            switch (_State)
            {
                case State.Noraml:
                    UpdateMainMenu(gameTime);
                    break;
                case State.Gallery:
                    Gallery.Update(gameTime);
                    break;
                case State.Setting:
                    break;
            }

            StateKeyboard.UpdateEnd();
        }

        private void DrawMainMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var button in buttons)
                button.Draw(gameTime, spriteBatch);
        }

        private void UpdateMainMenu(GameTime gameTime)
        {
            foreach (var button in buttons)
                button.Update(gameTime);

            if (StateKeyboard.CommandUp(Keys.Escape))
                game.Exit();
            if (StateKeyboard.CommandDown(Keys.Enter))
                game.ChangeState(new GameLevels(game, graphicsDevice, content, 1));
        }
    }
}
