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
    class Level20 : Level
    {
        private float timer = 20;
        Random random;

        public Level20(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level22";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 22;

            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Gravity = new Gravity((size, vector) => new Vector2(0.35f,0.35f));

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
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash6"),
                               new Vector2(25, 75),
                               this){
                               Speed=0.03f,
                               Timer=14f,
                               TurnTime = 14,
                               HPBar=130,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(200, LevelSize.Height - 155),
                               this){
                               Timer=4f,
                               TurnTime = 10,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new Mother(Content,Content.Load<Texture2D>("Sprite/Mother/Right/1"),new Vector2(LevelSize.Width - 300, 400),this),
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 28)
            {
                timer = 0;
                HostileMobs.Add(new Observer(Content,
                                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                                             new Vector2(-50, random.Next( 300, (int)LevelSize.Height / 2 + 200)), 
                                             this));
            }
        }
    }
}
