using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Levels;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Interfaces
{
    public class PauseMenu : Menu
    {
        public PauseMenu(Game1 game, ContentManager content, GraphicsDevice graphicsDevice, Level level)
            : base(game, content, graphicsDevice, level)
        {
            Texture = content.Load<Texture2D>("Interfaces/Pause/Backgraund");
            Location = new Vector2(level.LevelSize.Width / 2 - 345, level.LevelSize.Height / 2 - 400);

            Buttons = new List<Button>()
            {
                 new Button(content, new Vector2(Location.X+125, Location.Y+175), "Buttons/ContinueInMenu"){ Action = ()=>ToContinue()},
                 new Button(content, new Vector2(Location.X+173, Location.Y+300), "Buttons/Restart"){ Action = ()=>RestartLevel (level.IndexLevel)},
                 new Button(content, new Vector2(Location.X+183, Location.Y+425), "Buttons/Levels"){ Action=()=>ToLevelMap()},
                 new Button(content, new Vector2(Location.X+175, Location.Y+550), "Buttons/SettingInMenu"),
                 new Button(content, new Vector2(Location.X+190, Location.Y+675), "Buttons/MainMenu"){ Action = ()=>ToMainMenu()},
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Pause();

            if (level.StateKeyboard.CommandUp(Keys.Escape))
                ToMainMenu();
            if (level.StateKeyboard.CommandDown(Keys.Enter))
                ToContinue();
        }
    }
}
