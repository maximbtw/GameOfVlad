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
    class Level9 : Level
    {
        private float timer = 0;

        public Level9(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level10";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 10;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 10; i++)
                Stars.Add(new Star(Content, this));

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 150, LevelSize.Height - 150), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width / 2 - 100, LevelSize.Height / 2 - 100), 
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = new Vector2(LevelSize.Width / 2, LevelSize.Height / 2) - vector;
                var d = anomaly.Length();
                if (d > 0)
                    anomaly *= 1 / d;
                return anomaly * -140 * d / (d * d + 1);
            });

            var textureSpaceTrash = Content.Load<Texture2D>("Sprite/SpaceTrash/trash1");

            HostileMobs = new List<Mob>
            {
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2+50, LevelSize.Height / 2+50), 
                               this){
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed=0.3f,
                               Timer=5f},
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width / 2-100, LevelSize.Height / 2-400), 
                               this){ 
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed=5f,
                               Timer=2.5f,
                               TurnTime = 5f},
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width / 2-300, LevelSize.Height / 2-100), 
                               this){ 
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed=0.1f},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width / 2 - 100, LevelSize.Height / 2 - 100),
                               this){
                               State = SpaceTrash.StateDirection.Upright,
                               Speed=0.1f},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(300, 100),
                                this){
                                Speed = 3f},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(150, 500),
                                this){
                                Speed = 2.5f},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(150, 900),
                                this){
                                Speed = 2f},

                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 13)
            {
                timer = 0;
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(LevelSize.Width/2, -200),
                                                this));
            }
        }
    }
}
