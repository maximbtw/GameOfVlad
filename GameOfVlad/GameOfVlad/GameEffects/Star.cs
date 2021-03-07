using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Tools;
using GameOfVlad.Sprites;
using System;
using GameOfVlad.Levels;

namespace GameOfVlad.GameEffects
{
    public class Star : Sprite
    {
        private Random random;
        private Vector2 Direction;
        private List<Texture2D> Textures;
        public bool R = false;
        public bool G = false;
        public bool B = false;

        public Star(ContentManager content, Level level)
        {
            Content = content;
            Level = level;
            Textures = new List<Texture2D>();
            for (int i = 1; i < 5; i++)
            {
                Textures.Add(Content.Load<Texture2D>("Sprite/Stars/" + i));
            }
            random = new Random();
            Spawn();
        }

        public override void Update(GameTime gameTime)
        {
            Location += Direction;
            if (Location.Y > Level.LevelSize.Height + 200)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            Location = new Vector2(RandomV.GetRandom1(0, (int)Level.LevelSize.Width), RandomV.GetRandom1(-700, -100));
            Direction = new Vector2(RandomV.GetRandom1(-5, 5), RandomV.GetRandom1(1, 25));
            Texture = Textures[random.Next(Textures.Count)];
            Color = new Color(R ? (float)random.NextDouble() : 1,
                              G ? (float)random.NextDouble() : 1,
                              B ? (float)random.NextDouble() : 1,
                                  (float)random.NextDouble());
        }
    }
}
