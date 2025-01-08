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
    public class Level4 : Level
    {
        public Level4(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

            Name = "Level4";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 4;

            InitializeSprites();
        }
        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 3; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => Vector2.Zero);

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(LevelSize.Width / 2, 100),
                                this);

            Earth = new Sprite(Content,
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width / 2 - 100, 800),
                               this);

            HostileMobs = new List<Mob>()
            {
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash1"), 
                               new Vector2(LevelSize.Width / 2-75, 200), 
                               this) { 
                               State = SpaceTrash.StateDirection.Horizontally,
                               Timer=3.5f,
                               Speed = 1.1f },
                new SpaceTrash(Content, 
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash2"), 
                               new Vector2(LevelSize.Width-550, 800), 
                               this) { 
                               State = SpaceTrash.StateDirection.Upright,
                               Speed=0.75f,
                               TurnTime=5f,
                               Timer = 5f,
                               HPBar=60 },
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(50, 475),
                               this) {
                               State = SpaceTrash.StateDirection.Upright,
                               Speed=0.1f,
                               TurnTime=5f,
                               Timer = 5f,
                               HPBar=55 },
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash3"),
                               new Vector2(LevelSize.Width - 130, 550),
                               this) {
                               State = SpaceTrash.StateDirection.Upright,
                               Speed=0.1f,
                               Timer = 5f,
                               HPBar=55 },
                new SpaceTrash(Content,
                               Content.Load<Texture2D>("Sprite/SpaceTrash/trash4"),
                               new Vector2(LevelSize.Width / 2 - 225, 825),
                               this) {
                               State = SpaceTrash.StateDirection.Upright,
                               Speed=1.5f,
                               TurnTime=2f,
                               Timer = 2.5f,
                               HPBar=35 },
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(LevelSize.Width / 2 - 300, 830),
                                this){ 
                                Speed=2.3f},
                new SpaceKnight(Content,
                                Content.Load<Texture2D>("Sprite/SpaceKnight/Left/1"),
                                new Vector2(LevelSize.Width / 2 + 200, 830),
                                this),
            };
        }
    }
}
