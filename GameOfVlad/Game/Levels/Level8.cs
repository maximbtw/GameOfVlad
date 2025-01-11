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
    class Level8 : Level
    {
        private float timer1 = 0;
        private float timer2 = 10;
        private Random random;

        public Level8(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level8";
            IndexLevel = 8;

            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 3; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(-0.6f,0));

            Player = new Player(Content, Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                         new Vector2(50, LevelSize.Height / 2 + 25), this);

            Earth = new Sprite(Content, Content.Load<Texture2D>("Sprite/Earth/Earth"),
                new Vector2(LevelSize.Width - 225, LevelSize.Height / 2 - 100), this);

            HostileMobs = new List<Mob>
            {
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 400, 50),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 0.75f},
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 400, 300),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 0.75f},
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 700, 300),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 4.75f},
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 700, 550),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 4.75f},
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 1000, 550),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 8.75f},
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 1000, 800),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 8.75f},
            new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width - 1300, 925),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=60,
                               TurnTime = 11,
                               Speed = 1,
                               Timer = 11},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer2 > 15)
            {
                timer2 = 0;
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(LevelSize.Width + 500, random.Next(0, (int)LevelSize.Height)),
                                                this){
                                                Speed = (float)random.NextDouble() + 1});
            }
            if (timer1 > 25)
            {
                timer1 = 0;
                for (int i = 0; i < 3; i++)
                    HostileMobs.Add(new Meteorit(Content,
                                                 Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),
                                                 Vector2.Zero,
                                                 this));
            }
        }
    }
}
