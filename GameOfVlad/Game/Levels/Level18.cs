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
    class Level18 : Level
    {
        float timer = 40;

        public Level18(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level20";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 20;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true });

            Gravity = new Gravity((size, vector) => new Vector2(0.5f, -0.5f));

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 150, 80), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(50, LevelSize.Height - 175), 
                               this);


            HostileMobs = new List<Mob>
            {
                //new Mother(Content,Content.Load<Texture2D>("Sprite/Mother/Right/1"),new Vector2(300,400),this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this){ TimeRespawn=8 },
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this){ TimeRespawn=9 },
                new Fire(Content,Content.Load<Texture2D>("Pages/GamePlay/Speciallevel2//FireBeam2"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(160,175),
                               this){
                               Speed=3.55f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(400,300),
                               this){
                               Speed=1f,
                               TurnTime=10,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(700,440),
                               this){
                               Speed = 1.25f,
                               TurnTime = 7f,
                               Timer = 3,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(1000,575),
                               this){
                               Speed=1f,
                               Timer=7,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(1100,700),
                               this){
                               Speed = 1.5f,
                               TurnTime = 9f,
                               Timer = 3,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(1500,850),
                               this){
                               Speed = 2f,
                               TurnTime = 5f,
                               Timer = 3,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(200,850),
                               this){
                               Speed = 2.25f,
                               TurnTime = 6f,
                               Timer = 1,
                               State = SpaceTrash.StateDirection.Horizontally},
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 40)
            {
                timer = 0;
                HostileMobs.Add(new SpaceKnight(Content,
                                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                                new Vector2(-100, LevelSize.Height + 50), 
                                                this));
            }
        }
    }
}