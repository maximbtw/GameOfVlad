using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameEffects
{
    public class ScreenDimmingEffect
    {
        public Texture2D Backgraund;
        private int timeCounter = 0;
        private  Color color;
        public int Count = 512;

        public bool End = true;

        public ScreenDimmingEffect(Texture2D backgraund)
        {
            Backgraund = backgraund;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgraund, Vector2.Zero, color);
        }

        public void Update(GameTime gameTime)
        {
            if (timeCounter >= Count)
            {
                End = true;
            }
            else
            {
                color = new Color(255, 255, 255, timeCounter % 256);
                timeCounter += 5;
            }
        }

        public void Start(int count = 512)
        {
            End = false;
            Count = count;
            timeCounter = 0;
        }
    }
}
