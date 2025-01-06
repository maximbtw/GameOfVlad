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
    class Level12 : Level
    {
        public Level12(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level13";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 13;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 6; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => Vector2.Zero);

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(75, LevelSize.Height - 110),
                                this);

            Earth = new Sprite(Content, 
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width-225, 100), 
                               this);

            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new Meteorit(Content,Content.Load<Texture2D>("Sprite/Meteorit/Meteorit"),Vector2.Zero,this),
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width-425, 100),
                               this){
                               Speed=0.1f,
                               TurnTime=10f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"),
                               new Vector2(LevelSize.Width/2-300,500),
                               this){
                               Speed=5f,
                               TurnTime=5f,
                               Timer=2f,
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"),
                               new Vector2(LevelSize.Width-225, 240),
                               this){
                               Speed=0.09f,
                               HPBar = 60,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(75,50),
                               this){
                               HPBar = 55,
                               State = SpaceTrash.StateDirection.Upright},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(LevelSize.Width-250, LevelSize.Height-175),
                               this){
                               HPBar = 55,
                               Timer = 7,
                               State = SpaceTrash.StateDirection.Upright},
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(LevelSize.Width-750, 50),
                             this){
                             Speed=0.6f,
                             TimeToShot = 2.6f},
                new Observer(Content,
                             Content.Load<Texture2D>("Sprite/Observer/Left/1"),
                             new Vector2(LevelSize.Width-75, 500),
                             this){ 
                             Speed=0.4f,
                             TimeToShot = 2.4f},
            };
        }
    }
}
