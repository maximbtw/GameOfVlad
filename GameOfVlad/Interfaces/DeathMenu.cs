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
    public class DeathMenu : Menu
    {
        private Song music;
        public DeathMenu(GameOfVlad game, ContentManager content, GraphicsDevice graphicsDevice, Level level)
               : base(game, content, graphicsDevice, level)
        {
            Texture = content.Load<Texture2D>("Interfaces/Dead/Backgraund");
            music = content.Load<Song>("Interfaces/Dead/Music");

            Location = new Vector2(level.LevelSize.Width / 2 - 425, level.LevelSize.Height / 2 - 350);

            Buttons = new List<Button>()
            {
                  // new Button(content, new Vector2(Location.X+275, Location.Y+225), "Buttons/Restart"){ OnPressed = ()=>RestartLevel (level.IndexLevel)},
                  // new Button(content, new Vector2(Location.X+280, Location.Y+325), "Buttons/Levels"){ OnPressed=()=>ToLevelMap()},
                  // new Button(content, new Vector2(Location.X+200, Location.Y+425), "Buttons/Setting"),
                  // new Button(content, new Vector2(Location.X+280, Location.Y+525), "Buttons/MainMenu"){ OnPressed = ()=>ToMainMenu()},
            };
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
            //     RestartLevel(level.IndexLevel);
        }
    }
}
