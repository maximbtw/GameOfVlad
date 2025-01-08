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
    class Level3 : Level
    {
        public Level3(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

            Name = "Level3";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 3;

            InitializeSprites();
        }
        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 3; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(-0.1f, 0));

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(120, LevelSize.Height - 200),
                                this);

            Earth = new Sprite(Content,
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width - 300, 200),
                               this);

            HostileMobs = new List<Mob>()
            {
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(500, 300), 
                               this) { 
                               State = SpaceTrash.StateDirection.Horizontally,
                               Speed=1,
                               TurnTime=5.5f},
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width-350, 400), 
                               this) { 
                               State = SpaceTrash.StateDirection.Horizontally},
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"), 
                               new Vector2(1300, 200), 
                               this) { 
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=55,
                               Speed=0.25f,
                               Timer=2,
                               TurnTime=6},
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash4"), 
                               new Vector2(300, LevelSize.Height - 400), 
                               this) { 
                               State = SpaceTrash.StateDirection.Upright,
                               HPBar=30,
                               Speed=2f,
                               Timer=5.25f,
                               TurnTime=7.5f},
            };
        }
    }
}
