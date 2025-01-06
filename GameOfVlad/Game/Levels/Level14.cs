using System;
using System.Collections.Generic;
using GameOfVlad.GameEffects;
using GameOfVlad.Interfaces;
using GameOfVlad.Sprites;
using GameOfVlad.Sprites.Mobs;
using GameOfVlad.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels
{
    class Level14 : Level
    {
        private float timer = 20;
        private float timeToSpawn = 20;
        int observerCount = 0;
        private Random random;
        public Level14(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                  : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level15";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 15;
            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(0,0.4f));

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width/2-175, LevelSize.Height - 50), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width / 2 + 175, 45), 
                               this);


            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 + 175, 150),
                               this){
                               Speed=0.1f,
                               TurnTime=7f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 + 175, 250),
                               this){
                               Speed=0.1f,
                               TurnTime=7f,
                               Timer=7f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash5"),
                               new Vector2(40, LevelSize.Height/2-200),
                               this){
                               Speed=0.03f,
                               HPBar = 100,
                               TurnTime = 60f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width / 2 - 10, 50),
                               this){
                               Speed=0.1f,
                               HPBar = 60,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width-150, LevelSize.Height-175),
                               this){
                               HPBar = 60,
                               State = SpaceTrash.StateDirection.Posive},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > timeToSpawn)
            {
                timer = 0;
                observerCount++;
                if (observerCount != 3)
                    HostileMobs.Add(new Observer(Content,
                                                 Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                                                 (random.Next(1, 3) == 1) ? new Vector2(LevelSize.Width / 2 + 500, -200) :
                                                                            new Vector2(LevelSize.Width / 2 - 500, -200),this));
                else
                {
                    timeToSpawn = 60;
                    HostileMobs.Add(new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             (random.Next(1, 3) == 1) ? new Vector2(LevelSize.Width / 2 + 500, -200) :
                                                        new Vector2(LevelSize.Width / 2 - 500, -200), this,true));
                }
            }
        }
    }
}
