using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework.Media;
using GameOfVlad.UI.Button;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Interfaces
{
    public class CompliteMenu : Menu
    {
        private Song music;
        private SpriteFont font;
        public string BestTime {private get; set; }
        public string СurrentTime {private get; set; }

        public CompliteMenu(GameOfVlad game, ContentManager content, GraphicsDevice graphicsDevice, Level level)
            : base(game, content, graphicsDevice, level)
        {
            Texture = content.Load<Texture2D>("Interfaces/Complite/Backgraund");
            music = content.Load<Song>("Interfaces/Complite/Music");
            font = content.Load<SpriteFont>("Interfaces/Complite/Font");

            Location = new Vector2(level.LevelSize.Width / 2 - 425, level.LevelSize.Height / 2 - 350);

            Buttons = new List<Button>()
            {
                  // new Button(content, new Vector2(Location.X+227, Location.Y+225), "Buttons/ContinueInMenu"){ OnPressed = ()=>ToNextLevel(level.IndexLevel)},
                  // new Button(content, new Vector2(Location.X+268, Location.Y+415), "Buttons/Restart"){ OnPressed = ()=>RestartLevel (level.IndexLevel)},
                  // new Button(content, new Vector2(Location.X + 285, Location.Y + 525), "Buttons/MainMenu"){ OnPressed = ()=>ToMainMenu()},
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, СurrentTime + " c.", new Vector2(Location.X + 445, Location.Y + 68), Color.White);
            spriteBatch.DrawString(font, BestTime + " c.", new Vector2(Location.X+575, Location.Y+120), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            // base.Update(gameTime);
            //
            // if (game.CurrentMusic != music)
            //     game.NextMusic = music;
            //
            // if (level.KeyboardState.CommandUp(Keys.Escape))
            //     ToMainMenu();
            // if (level.KeyboardState.CommandDown(Keys.Enter))
            //     ToNextLevel(level.IndexLevel);
        }
    }
}
