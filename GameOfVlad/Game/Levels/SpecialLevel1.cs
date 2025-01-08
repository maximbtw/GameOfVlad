using System;
using System.Collections.Generic;
using GameOfVlad.GameEffects;
using GameOfVlad.Interfaces;
using GameOfVlad.Sprites;
using GameOfVlad.Sprites.Mobs;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels
{
    class SpecialLevel1 : Level
    {
        private float timer = 120;
        private SpriteFont font;
        private Random random;
        public List<bool> Stages;

        public SpecialLevel1(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
             : base(game, graphicsDevice, content)
        {
            SpecialLevel = true;
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/SpecialLevel1/Backgraund");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level9";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 9;

            random = new Random();
            font = content.Load<SpriteFont>("Interfaces/Complite/Font");

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for(int i = 0; i < 30; i++)
                Stars.Add(new Star(Content, this));

            Stages = new List<bool>();
            for (int i = 0; i < 4; i++)
                Stages.Add(true);

            Gravity = new Gravity((size, vector) => Vector2.Zero);

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width/2-30, LevelSize.Height-130), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width - 225, LevelSize.Height / 2 - 100), 
                               this);

            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
            };
        }

        private string time { get { var str = Convert.ToInt32(timer.ToString("N0"));
                                    var sec = str % 60;
                                    return (str / 60) + " : " + ((sec == 0) ? "00" : (sec < 10) ? "0" + sec  : sec.ToString()); } }

        public override void DrawPlay(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.DrawPlay(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "продержись:  "+time, new Vector2(LevelSize.Width / 2-148, 25), Color.Red);
        }
        public override void UpdatePlay(GameTime gameTime)
        {
            if(Player.turnState != Player.Turn.None)
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer < 0)
            {
                Save();
            }
            if (timer < 90 && Stages[0]) 
            {
                Stages[0] = false;
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
            }
            else if (timer < 60 && Stages[1]) 
            {
                Stages[1] = false;
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                SpawnKnight();
            }
            else if (timer < 30 && Stages[2]) 
            {
                Stages[2] = false;
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                SpawnKnight();
                SpawnKnight();
            }
            else if (timer < 15 && Stages[3])
            {
                Stages[3] = false;
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
                SpawnKnight();
                SpawnKnight();
                SpawnKnight();
            }

            base.UpdatePlay(gameTime);
        }

        private void SpawnKnight()
        {
            HostileMobs.Add(new SpaceKnight(Content,
                            Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                            new Vector2(random.Next(0, (int)LevelSize.Width), -100),
                            this){
                            Speed = (float)random.NextDouble() + 1.5f});
        }
    }
}
