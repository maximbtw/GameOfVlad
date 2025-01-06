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
    class Level11 : Level
    {
        private float timer = 15;
        private Random random;

        public Level11(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level12";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 12;
            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 5; i++)
                Stars.Add(new Star(Content, this));

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(125, 100),
                                this){ 
                                AccelerationMax = 2.5f };

            Earth = new Sprite(Content,
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(1700, LevelSize.Height-250),
                               this);

            BlackHole = new Sprite(Content,
                       Content.Load<Texture2D>("Sprite/BlackHole/2"),
                       (Player.Location+Earth.Location)*0.5f,
                       this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = new Vector2(BlackHole.Location.X + BlackHole.Texture.Width / 2, 
                                          BlackHole.Location.Y + BlackHole.Texture.Height / 2) - vector;
                var d = anomaly.Length();
                if (anomaly != Vector2.Zero)
                    anomaly.Normalize();
                return anomaly * 460 * d / (d * d + 1);
            });

            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),

                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(1625, LevelSize.Height-375),
                               this){
                               Speed=0.1f,
                               TurnTime=10f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(75,LevelSize.Height-175),
                               this){
                               Speed=4.75f,
                               TurnTime=5f,
                               Timer=0f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width/2+350,200),
                               this){
                               Speed=0.05f,
                               HPBar =60,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash4"),
                               new Vector2(1600, LevelSize.Height-250),
                               this){
                               Speed=0.02f,
                               HPBar =40,
                               State = SpaceTrash.StateDirection.Upright},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 16)
            {
                timer = 0;
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(LevelSize.Width + 500, random.Next(0, (int)LevelSize.Height)),
                                                this){
                                                Speed = (float)random.NextDouble() + 1});
            }
        }
    }
}
