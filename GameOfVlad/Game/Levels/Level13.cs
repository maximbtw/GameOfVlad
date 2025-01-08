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
    class Level13 : Level
    {
        private float timer = 20;
        private Random random;

        public Level13(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level14";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 14;

            random = new Random();
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this));

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 100, LevelSize.Height / 2 +100),
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width/2-550, LevelSize.Height/2-100), 
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = new Vector2(Earth.Location.X+Earth.Texture.Width, Earth.Location.Y + Earth.Texture.Height) - vector;
                var d = anomaly.Length();
                if (d > 0)
                    anomaly *= 1 / d;
                return anomaly * -180 * d / (d * d + 1);
            });


            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width/2-550, LevelSize.Height/2-200),
                               this){
                               Speed=0.3f,
                               TurnTime=7f,
                               Timer=3.5f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 - 550 + 40, LevelSize.Height/2+50),
                               this){
                               Speed=0.3f,
                               TurnTime=7f,
                               Timer=7f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash4"),
                               new Vector2(LevelSize.Width/2+100, LevelSize.Height-150),
                               this){
                               Speed=5f,
                               HPBar = 40,
                               Timer=3f,
                               TurnTime = 3f,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash4"),
                               new Vector2(LevelSize.Width/2-150,50),
                               this){
                               Speed=5f,
                               TurnTime = 3f,
                               HPBar = 40,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width/2-825, LevelSize.Height/2-100),
                               this){
                               Speed=0.05f,
                               TurnTime = 3f,
                               HPBar = 60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width/2-85, 18),
                               this){
                               Speed=0.01f,
                               TurnTime = 3f,
                               HPBar = 60,
                               State = SpaceTrash.StateDirection.Horizontally},
            };

            Walls = new List<Wall>
            {
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Platform/270"), new Vector2(1300, LevelSize.Height-616), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Platform/270"), new Vector2(670, LevelSize.Height-616), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Platform/270"), new Vector2(400, LevelSize.Height-453), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Platform/270"), new Vector2(127, LevelSize.Height-290), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Wall/Floor"), new Vector2(0, LevelSize.Height-127), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Wall/Wall"), new Vector2(0, 0), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Wall/Wall"), new Vector2(LevelSize.Width-127, 0), this),
                new Wall(Content, Content.Load<Texture2D>("PersonRun/Wall/Сeiling"), new Vector2(127, 0), this),
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 60)
            {
                timer = 0;
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(-200, random.Next((int)LevelSize.Height / 2, (int)LevelSize.Height)),
                                                this){ Speed = (float)random.NextDouble() + 1});
                HostileMobs.Add(new Observer(Content,
                                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                                             new Vector2(-200, random.Next(0, (int)LevelSize.Height / 2)),
                                             this){ TimeToShot = (float)random.NextDouble() + 2});
            }
        }
    }
}
