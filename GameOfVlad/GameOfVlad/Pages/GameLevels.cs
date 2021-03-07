using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using GameOfVlad.Levels;

namespace GameOfVlad.Pages
{
    public class GameLevels : State
    {
        private Level currentState;
        private Level nextState;
        private static List<Level> levels;
        public static int LevelCount { get { return 33; } }

        public GameLevels(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int indexLevel)
            : base(game, graphicsDevice, content, indexLevel)
        {
            levels = new List<Level>
            {
                new Level1(game,graphicsDevice,content),
                new Level2(game,graphicsDevice,content),
                new Level3(game,graphicsDevice,content),
                new Level4(game,graphicsDevice,content),
                new Level5(game,graphicsDevice,content),
                new Level6(game,graphicsDevice,content),
                new Level7(game,graphicsDevice,content),
                new Level8(game,graphicsDevice,content),
                new SpecialLevel1(game,graphicsDevice,content),
                new Level9(game,graphicsDevice,content),
                new Level10(game,graphicsDevice,content),
                new Level11(game,graphicsDevice,content),
                new Level12(game,graphicsDevice,content),
                new Level13(game,graphicsDevice,content),
                new Level14(game,graphicsDevice,content),
                new SpecialLevel2(game,graphicsDevice,content),
                new Level15(game,graphicsDevice,content),
                new Level16(game,graphicsDevice,content),
                new Level17(game,graphicsDevice,content),
                new Level18(game,graphicsDevice,content),
                new Level19(game,graphicsDevice,content),
                new Level20(game,graphicsDevice,content),
                new Level21(game,graphicsDevice,content),
                new Level22(game,graphicsDevice,content),
                new SpecialLevel3(game,graphicsDevice,content),
                new Level23(game,graphicsDevice,content),
                new Level24(game,graphicsDevice,content),
                new Level25(game,graphicsDevice,content),
                new Level26(game,graphicsDevice,content),
                new Level27(game,graphicsDevice,content),
                new Level28(game,graphicsDevice,content),
                new Level29(game,graphicsDevice,content),
                new Level30(game,graphicsDevice,content),
                new Level31(game,graphicsDevice,content),
            };

            currentState = levels[indexLevel - 1];
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            currentState.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            currentState.Update(gameTime);
        }
    }
}
