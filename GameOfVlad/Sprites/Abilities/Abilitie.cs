using GameOfVlad.Sprites.Mobs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameOfVlad.Game.Levels;

namespace GameOfVlad.Sprites.Abilities
{
    public class Abilitie : OldComponent
    {
        public bool Activ = false;
        protected ContentManager Content;
        protected Level Level;
        protected Mob Parent;
        protected Player Rocket;
        protected Action Action;
        protected int TTK;
        protected float ActionTime;

        protected float time = 0;
        protected int ttk;

        public Abilitie(ContentManager content, Level level, Mob parent, int ttk, float actionTime)
        {
            Content = content;
            Level = level;
            Parent = parent;
            this.ttk = TTK = ttk;
            ActionTime = actionTime;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {
            TTK--;
            if (TTK < 0)
            {
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time > ActionTime)
                {
                    time = 0;
                    TTK = ttk;
                }
                else if(Action!= null)
                    Action.Invoke();
            }
        }
    }
}
