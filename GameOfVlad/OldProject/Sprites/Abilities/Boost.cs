using System;
using System.Collections.Generic;
using GameOfVlad.Game.Levels;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.OldProject.Sprites.Abilities
{
    public class Boost : Abilitie
    {
        private bool update = true;
        private float speed;
        private ParticleEngine effect;

        public Boost(ContentManager content, Level level, Mob parent, int ttk, float actionTime)
            : base(content, level, parent, ttk, actionTime)
        {
            var texture = new List<Texture2D>
            {
                Content.Load<Texture2D>("Abilitie/Effect1"),
                Content.Load<Texture2D>("Abilitie/Effect2")
            };
            effect = new ParticleEngine(texture, 2, 7) 
            {
                R = 0.35f,
                G = 0.35f,
                B = 0.35f
            };

            if(parent is Player)
            {
                Rocket = (Player)parent;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Activ)
                effect.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            TTK--;
            if (TTK < 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Activ = true;
                if (Activ)
                    ActionBoost(gameTime);
            }

            effect.EmitterLocation = Rocket.Location;
            effect.Rotation = Rocket.Rotation;
            effect.Update(gameTime);
        }

        private void ActionBoost(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > ActionTime)
            {
                time = 0;
                TTK = ttk;
                update = true;
                Rocket.Speed = speed;
                Rocket.AccelerationMax = 4;
                Activ = false;
            }
            else
            {
                if (update)
                {
                    speed = Parent.Speed;
                    update = false;
                }

                var Speed = 10;
                Rocket.AccelerationMax = 10;

                var rotation = Rocket.Rotation - Math.PI/2;
                var Direction = new Vector2((float)Math.Cos(rotation), -(float)Math.Sin(-rotation));
                Rocket.Location += Speed * Direction;
            }
        }
    }
}
