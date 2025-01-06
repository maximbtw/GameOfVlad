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
    class Level7 : Level
    {
        public Level7(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level7";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 7;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 2; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(0, -300 / (300 + size.Height - vector.Y)));

            Player = new Player(Content, Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                     new Vector2(120, LevelSize.Height - 200), this);

            Earth = new Sprite(Content, Content.Load<Texture2D>("Sprite/Earth/Earth"),
                new Vector2(LevelSize.Width / 2 + 175, 325), this);

            var textureMeteorit = Content.Load<Texture2D>("Sprite/Meteorit/Meteorit");

            HostileMobs = new List<Mob>
            {
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"), 
                               new Vector2(300, 400), 
                               this){ 
                               State = SpaceTrash.StateDirection.Horizontally,
                               HPBar=60},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"), 
                               new Vector2(375, LevelSize.Height - 275), 
                               this){
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed = 0.75f,
                               HPBar=60},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width-400, 200), 
                               this){
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed = 0.25f,
                               Timer=4 },
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width/2-50, 300), 
                               this){
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed =1,
                               Timer=2.5f,
                               HPBar=10},
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width-800, 200), 
                               this){ 
                               State = SpaceTrash.StateDirection.Horizontally, 
                               Speed = 0.1f,
                               Timer=1 },
                new Meteorit(Content,textureMeteorit,new Vector2(-100,-200),this),
                new Meteorit(Content,textureMeteorit,new Vector2(-100,-200),this),
                new Meteorit(Content,textureMeteorit,new Vector2(-100,-200),this),
                new Meteorit(Content,textureMeteorit,new Vector2(-100,-200),this),
                new Meteorit(Content,textureMeteorit,new Vector2(-100,-200),this),
            };
        }
    }
}
