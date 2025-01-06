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
    class Level10 : Level
    {
        public Level10(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                    : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level11";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 11;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 10; i++)
                Stars.Add(new Star(Content, this));

            Player = new Player(Content, 
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width - 150, LevelSize.Height - 110), 
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(50, 50), 
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = new Vector2(50, 50) - vector;
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
                               new Vector2(200,30),
                               this){
                               Speed=0.25f,
                               TurnTime=40f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width-300,30),
                               this){
                               Speed=0.25f,
                               TurnTime=40f,
                               Timer=40f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash5"),
                               new Vector2(LevelSize.Width/2-250,LevelSize.Height/2-200),
                               this){
                               Speed=0.05f,
                               HPBar =100,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(25,200),
                               this){
                               Speed=0.1f,
                               Timer=10f,
                               HPBar=60,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash4"),
                               new Vector2(165,50),
                               this){
                               Speed=2f,
                               HPBar=40,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(-1750, 500),
                                this,
                                true),
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(124, 300),
                                this){
                                Speed = 2.3f},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(124, 900),
                                this){
                                Speed = 2f},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(-3000, -300),
                                this){
                                Speed = 2f},
            };
        }
    }
}
