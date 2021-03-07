using GameOfVlad.Levels;
using GameOfVlad.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace GameOfVlad.Sprites.Abilities
{
    public class Shield : Abilitie
    {
        public ColldesSprite _Shield;

        public Shield(ContentManager content, Level level, Mob parent, int ttk, float actionTime)
            : base(content, level, parent, ttk, actionTime)
        {
            if (parent is Player)
            {
                Rocket = (Player)parent;
            }
            _Shield = new ColldesSprite(Content, Content.Load<Texture2D>("Abilitie/Shield2"), Rocket.Location, Level);
            _Shield.Origin = new Vector2(_Shield.Texture.Width / 2, _Shield.Texture.Height / 2);
            _Shield.Direction = Rocket.Direction;
            _Shield.Rotation = Rocket.Rotation;
            _Shield.Location = Rocket.Location;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Activ)
                _Shield.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            TTK--;
            if (TTK < 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    Activ = true;
                if (Activ)
                    ActionShield(gameTime);
            }
        }

        private void ActionShield(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > ActionTime)
            {
                time = 0;
                TTK = ttk;
                Activ = false;
            }
            else
            {
                _Shield.Direction = Rocket.Direction;
                _Shield.Rotation = Rocket.Rotation;
                _Shield.Location = Rocket.Location;
            }
        }
    }
}
