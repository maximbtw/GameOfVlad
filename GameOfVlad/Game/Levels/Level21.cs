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
    class Level21 : Level
    {
        private float timer = -12;
        Random random;

        public Level21(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level23";
            IndexLevel = 23;

            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 300, LevelSize.Height - 300), 
                                this){
                                AccelerationMax = 2.5f};

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(60, LevelSize.Height/2), 
                               this);

            BlackHole = new Sprite(Content,
                                   Content.Load<Texture2D>("Sprite/BlackHole/2"),
                                   new Vector2(220, LevelSize.Height / 2 + 240),
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
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this),
                new Mother(Content,Content.Load<Texture2D>("Sprite/Mother/Right/1"),new Vector2(600, 500),this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(270, 530),
                               this){
                               Speed=1.25f,
                               Timer=7f,
                               TurnTime = 7,
                               HPBar=55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(30, 30),
                               this){
                               Speed=0.75f,
                               Timer=0f,
                               TurnTime = 7,
                               HPBar=55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(150, 300),
                               this){
                               Speed=0.75f,
                               Timer=3.5f,
                               TurnTime = 7,
                               HPBar=55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(400, 300),
                               this){
                               Speed=0.45f,
                               Timer=3.5f,
                               TurnTime = 18,
                               HPBar=55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(200, LevelSize.Height / 2 + 420),
                               this){
                               Speed = 0.25f,
                               Timer=4f,
                               TurnTime = 5,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 20)
            {
                timer = 0;
                HostileMobs.Add(new Observer(Content,
                                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                                             new Vector2(random.Next(200, (int)LevelSize.Width), -50),
                                             this));
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(-50, random.Next(500, (int)LevelSize.Height)),
                                                this));
            }
        }
    }
}

