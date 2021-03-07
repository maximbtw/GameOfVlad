using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameOfVlad
{
    public class Button : Component
    {
        public enum State
        {
            Normal,
            Hover,
            Pressed
        }

        protected ContentManager Content;
        protected Vector2 Location;
        protected string NameTexture;
        protected Rectangle BoundingBox;

        private bool active;
        public State _State;
        protected List<Texture2D> textures;
        public Action Action { get; set; }

        public Button(ContentManager content,Vector2 location,string nameTexture,bool active = true)
        {
            Content = content;
            Location = location;
            NameTexture = nameTexture;
            this.active = active;
            _State = State.Normal;
            LoadTexture();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (active!=null && !active)
            {
                spriteBatch.Draw(textures[textures.Count-1], Location, Color.White);
            }
            else if(_State == State.Hover && !NameTexture.Equals("Pages/MapLevels/Buttons/LevelSelect") 
                                    && !NameTexture.Equals("Buttons/ContinueInMenu")
                                    && !NameTexture.Equals("Buttons/SettingInMenu")
                                    && !NameTexture.Equals("Buttons/MainMenu")
                                    && !NameTexture.Equals("Pages/MapLevels/Buttons/SpecialLevel/LevelSelect"))
            {
                spriteBatch.Draw(textures[(int)_State], new Vector2(Location.X-8, Location.Y-3), Color.White);
            }
            else
                spriteBatch.Draw(textures[(int)_State], Location, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            if (BoundingBox.Contains(mouseState.X, mouseState.Y) && active)
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (Action != null && _State == State.Hover)
                        Action.Invoke();

                    _State = State.Pressed;
                }
                else
                    _State = State.Hover;
            else
                _State = State.Normal;
        }

        private void LoadTexture()
        {
            textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>(NameTexture));
            textures.Add(Content.Load<Texture2D>(NameTexture+1));
            if(!active)
                textures.Add(Content.Load<Texture2D>(NameTexture + 2));
            else textures.Add(Content.Load<Texture2D>(NameTexture));

            BoundingBox = new Rectangle((int)Location.X, (int)Location.Y, textures[0].Width, textures[0].Height);
        }
    }
}
