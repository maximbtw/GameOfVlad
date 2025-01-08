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
    class Level6 : Level
    {
        public Level6(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
                     : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level6";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 6;
            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 3; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(-0.6f,0.6f));

            Player = new Player(Content, Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                     new Vector2(120, LevelSize.Height - 200), this);

            Earth = new Sprite(Content, Content.Load<Texture2D>("Sprite/Earth/Earth"),
                new Vector2(LevelSize.Width - 300, 200), this);

            var textureMeteorit = Content.Load<Texture2D>("Sprite/Meteorit/Meteorit");


            HostileMobs = new List<Mob>
            {
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
                new Meteorit(Content,textureMeteorit, Vector2.Zero, this),
            };
        }
    }
}
