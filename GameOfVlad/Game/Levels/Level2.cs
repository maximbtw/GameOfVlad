using System.Collections.Generic;
using GameOfVlad.OldProject;
using GameOfVlad.OldProject.GameEffects;
using GameOfVlad.OldProject.Interfaces;
using GameOfVlad.OldProject.Sprites;
using GameOfVlad.OldProject.Sprites.Mobs;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels
{
    class Level2 : Level
    {
        public Level2(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

            Name = "Level2";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 2;

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 3; i++)
                Stars.Add(new Star(Content, this));

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(120, 200),
                                this);

            Earth = new Sprite(Content,
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(320, LevelSize.Height - 200),
                               this);

            Gravity = new Gravity((size, vector) =>
            {
                var anomaly = Earth.Location - vector;
                var d = anomaly.Length();
                if (d > 0)
                    anomaly *= 1 / d;
                return anomaly * -180 * d / (d * d + 1);
            }
            );
            HostileMobs = new List<Mob>();
        }
    }
}
