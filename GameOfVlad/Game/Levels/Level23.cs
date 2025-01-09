using System;
using System.Collections.Generic;
using GameOfVlad.OldProject;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Interfaces;
using GameOfVlad.OldProject.Sprites;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels
{
    public class Level23 : Level
    {
        private float timer = 10;
        Random random;

        public Level23(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund5");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

            Name = "Level26";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 26;

            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true , G = true});

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(100, LevelSize.Height - 200),
                                this);

            Earth = new Sprite(Content,
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width - 200, 100),
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = new Vector2(Earth.Location.X + Earth.Texture.Width, Earth.Location.Y + Earth.Texture.Height) - vector;
                var d = anomaly.Length();
                if (d > 0)
                    anomaly *= 1 / d;
                return anomaly * -160 * d / (d * d + 1);
            });

            HostileMobs = new List<Mob>()
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash5"),
                               new Vector2(LevelSize.Width - 400, 200),
                               this){
                               Speed=0.25f,
                               HPBar=100,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash5"),
                               new Vector2(LevelSize.Width - 500, 100),
                               this){
                               Speed=0.25f,
                               Timer=7f,
                               TurnTime = 7,
                               HPBar=100,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 1000, 800),
                               this){
                               Speed=2f,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 1100, 800),
                               this){
                               Speed=1.9f,
                               Timer=7f,
                               TurnTime = 7,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Upright},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 18)
            {
                timer = 0;
                HostileMobs.Add(new Butler(Content, 
                                           Content.Load<Texture2D>("Sprite/Butler/Left/1"), 
                                           new Vector2(LevelSize.Width + 50, random.Next(500, 1000)), 
                                           this));
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(random.Next(800, 1400), -50),
                                                this));
            }
        }
    }
}