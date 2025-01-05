using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Tools;
using GameOfVlad.Interfaces;
using GameOfVlad.Sprites;
using GameOfVlad.Sprites.Mobs;
using GameOfVlad.GameEffects;
using GameOfVlad.Sprites.Mobs.Boss;
using System;

namespace GameOfVlad.Levels
{
    class SpecialLevel2 : Level
    {
        Random random;
        int StageIndex = 0;

        public SpecialLevel2(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
      : base(game, graphicsDevice, content)
        {
            SpecialLevel = true;
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/SpecialLevel3/Backgraund");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);

            Name = "Level16";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 16;

            random = new Random();

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 8; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => new Vector2(0, 0));

            Player = new Player(Content, Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                         new Vector2(LevelSize.Width-150, LevelSize.Height - 150), this);

            HostileMobs = new List<Mob>
            {
                new Ship(Content,Content.Load<Texture2D>("Pages/GamePlay/SpecialLevel1/Ship"),new Vector2(-600,275),this),
            };
        }

        public override void UpdatePlay(GameTime gameTime)
        {
            base.UpdatePlay(gameTime);

            var mobCount = 0;
            Ship ship = null;

            foreach (var mob in HostileMobs)
            {
                mobCount++;
                if (mob is Ship)
                {
                    ship = (Ship)mob;
                    if (ship.CompliteLevel)
                        Save();
                }
            }

            if (mobCount == 1 && ship != null && ship.Stages[0] && !ship.Stages[11])
            {
                ship.Stages[StageIndex] = true;
                StageIndex++;
            }
        }
    }
}
