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
    class Level15 : Level
    {
        public Level15(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgroand2");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level17";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 17;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this) { B = true});

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 150, LevelSize.Height - 50), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(50, 50), 
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = Earth.Location - vector;
                var d = anomaly.Length();
                if (d > 0)
                    anomaly *= 1 / d;
                return anomaly * -180 * d / (d * d + 1);
            });

            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(200,25),
                               this){
                               Speed=0.05f,
                               Timer=7f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(25,170),
                               this){
                               Speed=0.05f,
                               TurnTime=9f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(25,LevelSize.Height-150),
                               this){
                               Speed=12f,
                               TurnTime=2.5f,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(LevelSize.Width/2+100,20),
                               this){
                               Speed=0.3f,
                               TurnTime=3f,
                               HPBar=55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(LevelSize.Width/2-100,20),
                               this){
                               Speed=0.3f,
                               TurnTime=9f,
                               HPBar=55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 + 25,LevelSize.Height / 2 - 150),
                               this){
                               Speed=0.1f,
                               Timer=7f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width / 2 - 75,LevelSize.Height/2+50),
                               this){
                               Speed=0.1f,
                               TurnTime=9f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(75,400),
                             this){ 
                             Speed = 0.75f },
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(75,800),
                             this){
                             TimeToShot = 2.35f },
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(-400,-100),
                             this,
                             true),
            };
        }
    }
}
