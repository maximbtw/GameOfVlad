using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameOfVlad.Tools
{
    public class Animation
    {
        private Texture2D texture { get; set; }
        private List<Texture2D> textures;
        private int countTexture;
        private static int index = 0;

        float Timer;
        float time;

        public bool Next;
        public Texture2D GetTexture { get { Next = false; return texture; } }

        public Animation(ContentManager content, string nameTexture, float time, int countTexture)
        {
            this.countTexture = countTexture;
            Timer = this.time = time;
            Next = false;

            textures = new List<Texture2D>();
            for (int i = 1; i < countTexture + 1; i++)
            {
                textures.Add(content.Load<Texture2D>(nameTexture + i.ToString()));
            }
        }

        public void Update(GameTime gameTime)
        {
            Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds; 
            if (Timer < 0)
            {
                index++;
                texture = textures[index % countTexture];
                Timer = time;
                Next = true;
            }
        }
    }
}
