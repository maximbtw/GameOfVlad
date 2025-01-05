using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Tools;
using GameOfVlad.Interfaces;
using GameOfVlad.Sprites;
using GameOfVlad.Sprites.Mobs;
using System;
using GameOfVlad.GameEffects;

namespace GameOfVlad.Levels
{
    class Level5 : Level
    {
        private float timer = 9;
        private Random random;

        public Level5(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level5";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 5;

            random = new Random();

            InitializeSprites();
        }
        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 2; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(0.3f, -0.1f));

            Player = new Player(Content, Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                     new Vector2(120, LevelSize.Height - 200), this);

            Earth = new Sprite(Content, Content.Load<Texture2D>("Sprite/Earth/Earth"),
                new Vector2(LevelSize.Width - 300, 200), this);

            HostileMobs = new List<Mob>
            {
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(300, 400), 
                               this)  { 
                               State = SpaceTrash.StateDirection.Horizontally,},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width-350, 400), 
                               this){
                               State = SpaceTrash.StateDirection.Horizontally,
                               Speed = 0.75f},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width-400, 200), 
                               this){
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed = 0.25f,
                               Timer=4 },
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width-350, 0), 
                               this){ State = SpaceTrash.StateDirection.Horizontally,
                               Timer=2},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 15)
            {
                Spawn();
                timer = 0;
            }
        }

        private void Spawn()
        {
            HostileMobs.Add(new SpaceKnight(Content,
                                            Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                            new Vector2(LevelSize.Width + 500, random.Next(0, (int)LevelSize.Height)),
                                            this) {
                                            Speed = (float)random.NextDouble() + 1});
            HostileMobs.Add(new SpaceKnight(Content,
                                            Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                            new Vector2(random.Next(0, (int)LevelSize.Width), -500),
                                            this){ 
                                            Speed = (float)random.NextDouble()+1});
        }
    }
}
