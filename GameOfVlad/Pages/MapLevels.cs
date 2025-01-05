using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using GameOfVlad.Tools;

namespace GameOfVlad.Pages
{
    public class MapLevels : State
    {
        private List<Button> buttons;
        private Texture2D backgraund;
        private SpriteFont font;

        public MapLevels(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            StateKeyboard = new StateKeyboard();

            backgraund = content.Load<Texture2D>("Pages/MapLevels/Backgraund2");
            font = content.Load<SpriteFont>("Pages/MapLevels/Font");

            var buttonName = "Pages/MapLevels/Buttons/LevelSelect";

            buttons = new List<Button>
            {
                new Button(content,new Vector2(850,backgraund.Height-105),buttonName)
                                           { Action = () => ToLevelSelect(1)},
                new Button(content,new Vector2(754,backgraund.Height-165),buttonName,
                    game.DataCenter.Level1){ Action = () => ToLevelSelect(2)},
                new Button(content,new Vector2(648,backgraund.Height-200),buttonName,
                    game.DataCenter.Level2){ Action = () => ToLevelSelect(3)},
                new Button(content,new Vector2(540,backgraund.Height-215),buttonName,
                    game.DataCenter.Level3){ Action = () => ToLevelSelect(4)},
                new Button(content,new Vector2(428,backgraund.Height-205),buttonName,
                    game.DataCenter.Level4){ Action = () => ToLevelSelect(5)},
                new Button(content,new Vector2(320,backgraund.Height-178),buttonName,
                    game.DataCenter.Level5){ Action = () => ToLevelSelect(6)},
                new Button(content,new Vector2(214,backgraund.Height-170),buttonName,
                    game.DataCenter.Level6){ Action = () => ToLevelSelect(7)},
                new Button(content,new Vector2(120,backgraund.Height-230),buttonName,
                    game.DataCenter.Level7){ Action = () => ToLevelSelect(8)},
                new Button(content,new Vector2(180,backgraund.Height-347),"Pages/MapLevels/Buttons/SpecialLevel/LevelSelect",
                    game.DataCenter.Level8){ Action = () => ToLevelSelect(9)},
                new Button(content,new Vector2(260,backgraund.Height-460),buttonName,
                    game.DataCenter.SpecialLevel1){ Action = () => ToLevelSelect(10)},
                new Button(content,new Vector2(370,backgraund.Height-475),buttonName,
                    game.DataCenter.Level9){ Action = () => ToLevelSelect(11)},
                new Button(content,new Vector2(480,backgraund.Height-465),buttonName,
                    game.DataCenter.Level10){ Action = () => ToLevelSelect(12)},
                new Button(content,new Vector2(590,backgraund.Height-490),buttonName,
                    game.DataCenter.Level11){ Action = () => ToLevelSelect(13)},
                new Button(content,new Vector2(700,backgraund.Height-540),buttonName,
                    game.DataCenter.Level12){ Action = () => ToLevelSelect(14)},
                new Button(content,new Vector2(710,backgraund.Height-645),buttonName,
                    game.DataCenter.Level13){ Action = () => ToLevelSelect(15)},
                new Button(content,new Vector2(620,backgraund.Height-707),"Pages/MapLevels/Buttons/SpecialLevel/LevelSelect",
                    game.DataCenter.Level14){ Action = () => ToLevelSelect(16)},
                new Button(content,new Vector2(530,backgraund.Height-745),buttonName,
                    game.DataCenter.Level15){ Action = () => ToLevelSelect(17)},
                new Button(content,new Vector2(428,backgraund.Height-755),buttonName,
                    game.DataCenter.Level16){ Action = () => ToLevelSelect(18)},
                new Button(content,new Vector2(328,backgraund.Height-715),buttonName,
                    game.DataCenter.Level17){ Action = () => ToLevelSelect(19)},
                new Button(content,new Vector2(238,backgraund.Height-640),buttonName,
                    game.DataCenter.Level18){ Action = () => ToLevelSelect(20)},
                new Button(content,new Vector2(138,backgraund.Height-600),buttonName,
                    game.DataCenter.Level19){ Action = () => ToLevelSelect(21)},
                new Button(content,new Vector2(38,backgraund.Height-640),buttonName,
                    game.DataCenter.Level20){ Action = () => ToLevelSelect(22)},
                new Button(content,new Vector2(10,backgraund.Height-740),buttonName,
                    game.DataCenter.Level21){ Action = () => ToLevelSelect(23)},
                new Button(content,new Vector2(28,backgraund.Height-840),buttonName,
                    game.DataCenter.Level22){ Action = () => ToLevelSelect(24)},
                new Button(content,new Vector2(130,backgraund.Height-920),"Pages/MapLevels/Buttons/SpecialLevel/LevelSelect",
                    game.DataCenter.SpecialLevel3){ Action = () => ToLevelSelect(25)},

                new Button(content,new Vector2(245,backgraund.Height-1020),buttonName,
                    game.DataCenter.Level23){ Action = () => ToLevelSelect(26)},
                new Button(content,new Vector2(355,backgraund.Height-1045),buttonName,
                    game.DataCenter.Level24){ Action = () => ToLevelSelect(27)},

                new Button(content, new Vector2(465, backgraund.Height - 1044), buttonName,
                    game.DataCenter.Level25){ Action = () => ToLevelSelect(28) },
                new Button(content, new Vector2(572, backgraund.Height - 1007), buttonName,
                    game.DataCenter.Level26){ Action = () => ToLevelSelect(29) },
                new Button(content, new Vector2(672, backgraund.Height - 956), buttonName,
                    game.DataCenter.Level27){ Action = () => ToLevelSelect(30) },

                new Button(content, new Vector2(753, backgraund.Height - 875), buttonName,
                    game.DataCenter.Level28){ Action = () => ToLevelSelect(31) },

                new Button(content, new Vector2(795, backgraund.Height - 773), buttonName,
                    game.DataCenter.Level29){ Action = () => ToLevelSelect(32) },
                new Button(content, new Vector2(825, backgraund.Height - 670), buttonName,
                    game.DataCenter.Level30){ Action = () => ToLevelSelect(33) },

                new Button(content, new Vector2(845, backgraund.Height - 562), buttonName,
                    game.DataCenter.Level31){ Action = () => ToLevelSelect(34) },
            };

          //  Mouse.SetCursor(MouseCursor.Arrow);
        }

        private void ToLevelSelect(int indexLevel)
        {
            game.Components.Clear();
            game.ChangeState(new GameLevels(game, graphicsDevice, content, indexLevel));
        }
        //Для того чтобы
        //Цифра была под иконкой уровня
        //x+=38
        //y+=50
        //x+=26 для двйоных чисел
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgraund, new Rectangle(0, 0, 1920, 1080), Color.White);

            spriteBatch.DrawString(font, "1", new Vector2(850 + 38, backgraund.Height - 105 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "2", new Vector2(754 + 38, backgraund.Height - 165 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "3", new Vector2(648 + 38, backgraund.Height - 200 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "4", new Vector2(540 + 38, backgraund.Height - 215 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "5", new Vector2(428 + 38, backgraund.Height - 205 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "6", new Vector2(320 + 38, backgraund.Height - 178 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "7", new Vector2(214 + 38, backgraund.Height - 170 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "8", new Vector2(120 + 38, backgraund.Height - 230 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "9", new Vector2(260 + 38, backgraund.Height - 460 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "10", new Vector2(370 + 26, backgraund.Height - 475 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "??", new Vector2(177 + 28, backgraund.Height - 347 + 50), Color.DarkRed);
            spriteBatch.DrawString(font, "11", new Vector2(480 + 26, backgraund.Height - 465 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "12", new Vector2(590 + 26, backgraund.Height - 490 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "13", new Vector2(700 + 26, backgraund.Height - 540 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "14", new Vector2(710 + 26, backgraund.Height - 645 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "??", new Vector2(620 + 28, backgraund.Height - 707 + 50), Color.DarkRed);
            spriteBatch.DrawString(font, "15", new Vector2(530 + 26, backgraund.Height - 745 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "16", new Vector2(428 + 26, backgraund.Height - 755 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "17", new Vector2(328 + 26, backgraund.Height - 715 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "18", new Vector2(238 + 26, backgraund.Height - 640 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "19", new Vector2(138 + 26, backgraund.Height - 600 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "20", new Vector2(38 + 28, backgraund.Height - 640 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "21", new Vector2(10 + 28, backgraund.Height - 740 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "22", new Vector2(28 + 28, backgraund.Height - 840 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "??", new Vector2(130 + 28, backgraund.Height - 920 + 50), Color.LightGray);
            spriteBatch.DrawString(font, "23", new Vector2(245 + 28, backgraund.Height - 1020 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "24", new Vector2(355 + 28, backgraund.Height - 1045 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "25", new Vector2(465 + 28, backgraund.Height - 1044 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "26", new Vector2(572 + 28, backgraund.Height - 1007 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "27", new Vector2(672 + 28, backgraund.Height - 956 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "28", new Vector2(753 + 28, backgraund.Height - 875 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "29", new Vector2(795 + 28, backgraund.Height - 773 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "30", new Vector2(825 + 28, backgraund.Height - 670 + 50), Color.AliceBlue);
            spriteBatch.DrawString(font, "31", new Vector2(845 + 28, backgraund.Height - 562 + 50), Color.AliceBlue);

            foreach (var button in buttons)
                button.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            StateKeyboard.UpdateStart();

            foreach (var button in buttons)
                button.Update(gameTime);

            if (StateKeyboard.CommandUp(Keys.Escape))
                game.ChangeState(new MainMenu(game, graphicsDevice, content));

            StateKeyboard.UpdateEnd();
        }
    }
}
