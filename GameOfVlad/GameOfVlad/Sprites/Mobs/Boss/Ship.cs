using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameOfVlad.Levels;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using GameOfVlad.GameEffects;

namespace GameOfVlad.Sprites.Mobs.Boss
{
    class Ship : Mob
    {
        Random random;
        Vector2 position = new Vector2(15,15);
        public bool CompliteLevel = false;
        public List<bool> Stages;
        private ScreenDimmingEffect screenDimmingEffect;

        public Ship(ContentManager content, Texture2D texture, Vector2 location, Level level)
        : base(content, texture, location, level)
        {
            HPBar = 100000;
            random = new Random();

            Stages = new List<bool>();
            for (int i = 0; i < 12; i++)
                Stages.Add(false);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Location.X < position.X && !Stages[0])
            {
                Direction = position-Location;
                if (Direction != Vector2.Zero)
                    Direction.Normalize();
                Location += Direction * 2;
            }
            else
            {
                Stages[0] = true;
            }
            if (Stages[0])
            {

                if (Stages[1])
                {
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    Stages[1] = false;
                }
                else if (Stages[2])
                {
                    SpawnObserver();
                    Stages[2] = false;
                }
                else if (Stages[3])
                {
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    Stages[3] = false;
                }
                else if (Stages[4])
                {
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    SpawnObserver();
                    Stages[4] = false;
                }
                else if (Stages[5])
                {
                    SpawnObserver();
                    SpawnObserver();
                    Stages[5] = false;
                }
                else if (Stages[6])
                {
                    SpawnSpaceKnight(true);
                    SpawnSpaceKnight();
                    Stages[6] = false;
                }
                else if (Stages[7])
                {
                    SpawnObserver(true);
                    Stages[7] = false;
                }
                else if (Stages[8])
                {
                    SpawnSpaceKnight(true);
                    SpawnSpaceKnight();
                    SpawnObserver();
                    Stages[8] = false;
                }
                else if (Stages[9])
                {
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    SpawnObserver(true);
                    Stages[9] = false;
                }
                else if (Stages[10])
                {
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    SpawnSpaceKnight();
                    Stages[10] = false;
                }
                else if (Stages[11])
                {
                    Direction = new Vector2(630 * 1.5f, -245 * 1.5f) - Location;
                    if (Direction != Vector2.Zero)
                        Direction.Normalize();
                    Location += Direction * 2;
                    if (Location.X > 630 * 1.5f)
                    {
                        CompliteLevel = true;
                    }
                }
            }
        }

        void SpawnSpaceKnight(bool boss = false)
        {
            Level.HostileMobs.Add(new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(random.Next(-100,15), random.Next(-100,15)),
                                Level, 
                                boss){ 
                                Speed = (float)random.NextDouble() + 1.25f });
        }

        void SpawnObserver(bool boss = false)
        {
            Level.HostileMobs.Add(new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(random.Next(-10, 75), random.Next(-10, 75)),
                             Level, 
                             boss){ 
                             TimeToShot = (float)random.NextDouble() + 2.2f });
        }
    }
}
