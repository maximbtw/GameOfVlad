using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Tools;
using GameOfVlad.Levels;
using Microsoft.Xna.Framework.Input;
using GameOfVlad.Pages;
using System;

namespace GameOfVlad.Interfaces
{
    public class Gallery : Menu
    {
        public List<GalleryRecords> Records;
        private readonly Vector2 position1 = new Vector2(85f, 200f);
        private readonly Vector2 position2 = new Vector2(485f, 200f);
        private readonly Vector2 position3 = new Vector2(885f, 200f);
        private readonly Vector2 position4 = new Vector2(85f, 450f);
        private readonly Vector2 position5 = new Vector2(1f, 1f);
        private readonly Vector2 position6 = new Vector2(1f, 1f);
        private readonly Vector2 position7 = new Vector2(1f, 1f);
        private readonly Vector2 position8 = new Vector2(1f, 1f);
        private readonly Vector2 position9 = new Vector2(1f, 1f);

        private bool start = true;
        public Gallery(Game1 game, ContentManager content, GraphicsDevice graphicsDevice, MainMenu mainMenu)
            : base(game, content, graphicsDevice, mainMenu) 
        {
            Texture = content.Load<Texture2D>("Interfaces/Gallery/Backgraund");
            Location = new Vector2(65, 100);
            Buttons = new List<Button>
            {
                new ButtonGallery(content,new Vector2(65,100),"Interfaces/Gallery/ButtonRecords")
                                    {Action = ()=>RecordsPage1() },
                new ButtonGallery(content,new Vector2(665,100),"Interfaces/Gallery/ButtonWeapons")
                                    {Action = ()=>WeaponsPage1() },
            };
            Records = new List<GalleryRecords>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            foreach (var record in Records)
                record.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var record in Records)
                record.Update(gameTime);

            if (start)
            {
                RecordsPage1();
                start = false;
            }
            if (mainMenu.StateKeyboard.CommandUp(Keys.Escape))
                mainMenu._State = MainMenu.State.Noraml;

        }

        private void WeaponsPage1()
        {
            Buttons[0]._State = Button.State.Normal;
            Records.Clear();
        }

        private void RecordsPage1()
        {
            Buttons[1]._State = Button.State.Normal;
            Records.Clear();
            Records.Add(new GalleryRecords(content, position1, "Interfaces/Gallery/Records/SpaceTrash",
                        game.DataCenter.Level4));
            Records.Add(new GalleryRecords(content, position2, "Interfaces/Gallery/Records/Meteorit",
                        game.DataCenter.Level6));
            Records.Add(new GalleryRecords(content, position3, "Interfaces/Gallery/Records/Observer",
                        game.DataCenter.Level8));
            Records.Add(new GalleryRecords(content, position4, "Interfaces/Gallery/Records/ObserverMiniBoss",
                        game.DataCenter.Level10));
        }

        public class GalleryRecords : Button
        {
            private bool active;

            public GalleryRecords(ContentManager content, Vector2 location, string nameTexture, bool active = true)
                     : base(content, location, nameTexture)
            {
                this.active = active;
                textures = new List<Texture2D>
                {
                    content.Load<Texture2D>(nameTexture),
                    content.Load<Texture2D>(nameTexture+1),
                };
                BoundingBox = new Rectangle((int)Location.X, (int)Location.Y, textures[0].Width, textures[0].Height);
            }

            public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                if(active)
                    spriteBatch.Draw(textures[1], Location, Color.White);
                else
                    spriteBatch.Draw(textures[0], Location, Color.White);
            }

            public override void Update(GameTime gameTime)
            {
                var mouseState = Mouse.GetState();

                if (BoundingBox.Contains(mouseState.X, mouseState.Y) && active)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (Action != null && _State == State.Hover)
                            Action.Invoke();
                    }
                }
            }
        }

        private class ButtonGallery : Button
        {

            public ButtonGallery(ContentManager content, Vector2 location, string nameTexture) 
                : base(content,location,nameTexture)
            {
                _State = State.Normal;
                textures = new List<Texture2D>();
                textures.Add(Content.Load<Texture2D>(NameTexture));
                textures.Add(Content.Load<Texture2D>(NameTexture + 1));
                BoundingBox = new Rectangle((int)Location.X, (int)Location.Y, textures[0].Width, textures[0].Height);
            }

            public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                base.Draw(gameTime, spriteBatch);
            }

            public override void Update(GameTime gameTime)
            {
                var mouseState = Mouse.GetState();

                if (BoundingBox.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (Action != null && _State == State.Hover)
                            Action.Invoke();
                        _State = State.Hover;
                    }
                }
            }
        }
    }
}
