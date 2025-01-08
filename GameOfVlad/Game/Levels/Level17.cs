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
    class Level17 : Level
    {
        private float timer = 10;

        public Level17(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level19";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 19;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Gravity = new Gravity((size, vector) => new Vector2(0,0.45f));

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(50, LevelSize.Height - 110), 
                                this);

            Earth = new Sprite(Content, 
                              Content.Load<Texture2D>("Sprite/Earth/Earth"),
                              new Vector2(1300, 50), 
                              this);

            HostileMobs = new List<Mob>
            {
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash5"),
                               new Vector2(1100,25),
                               this){
                               Speed=0.07f,
                               HPBar=100,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash5"),
                               new Vector2(1500,100),
                               this){
                               Speed = 0.1f,
                               TurnTime = 3f,
                               Timer = 3,
                               HPBar=100,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(50,25),
                               this){
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width - 150, LevelSize.Height - 170),
                               this){
                               Timer = 7,
                               State = SpaceTrash.StateDirection.Horizontally},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 15)
            {
                timer = 0;
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(LevelSize.Width / 2, -200),
                                                this));
                HostileMobs.Add(new Observer(Content,
                                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                                             new Vector2(LevelSize.Width , 500),
                                             this){
                                             Speed = 0.4f,
                                             TimeToShot = 2.4f});
            }
        }
    }
}