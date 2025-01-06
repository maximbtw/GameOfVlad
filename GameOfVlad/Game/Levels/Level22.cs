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
    class Level22 : Level
    {
        float timer = 20;

        public Level22(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level24";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 24;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Gravity = new Gravity((size, vector) => Vector2.Zero);

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 150, LevelSize.Height - 110), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(50, 50), 
                               this);


            HostileMobs = new List<Mob>
            {
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash6"),
                               new Vector2(25, 40),
                               this){
                               Speed=0.03f,
                               Timer=3f,
                               TurnTime = 6,
                               HPBar=130,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash6"),
                               new Vector2(100, -50),
                               this){
                               Speed=0.01f,
                               Timer=10f,
                               TurnTime = 10,
                               HPBar=130,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash6"),
                               new Vector2(-50, 145),
                               this){
                               Speed=0.03f,
                               Timer=0,
                               TurnTime = 5,
                               HPBar=130,
                               State = SpaceTrash.StateDirection.Upright},
                new Mother(Content,
                           Content.Load<Texture2D>("Sprite/Mother/Right/1"),
                           new Vector2(LevelSize.Width / 2, LevelSize.Height / 2 - 153),
                           this){   
                           SpawnTime = 3.5f},
                new Mother(Content,
                           Content.Load<Texture2D>("Sprite/Mother/Right/1"),
                           new Vector2(LevelSize.Width / 2, LevelSize.Height / 2 + 150),
                           this){ 
                           SpawnTime = 3.25f},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);

            var mother = true;
            foreach(var mob in HostileMobs)
                if (mob is Mother)
                {
                    mother = false;
                    break;
                }
            if (mother)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 20)
                {
                    timer = 0;
                    Spawn(new Vector2(-50, 600));
                    Spawn(new Vector2(600, -50));
                }
            }                
        }

        void Spawn(Vector2 location)
        {
            HostileMobs.Add(new Meteorit(Content, Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"), Vector2.Zero, this));
            HostileMobs.Add(new Observer(Content,
                            Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                            location,
                            this));
        }
    }
}

