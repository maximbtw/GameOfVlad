﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameOfVlad.Tools;
using GameOfVlad.Interfaces;
using GameOfVlad.Sprites;
using GameOfVlad.Sprites.Mobs;
using GameOfVlad.GameEffects;

namespace GameOfVlad.Levels
{
    public class Level30 : Level
    {
        public Level30(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

            Name = "Level33";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 33;

            InitializeSprites();
        }

        public override void InitializeSprites()
        {
            Stars = new List<Star>();
            for (int i = 0; i < 5; i++)
                Stars.Add(new Star(Content, this));

            Gravity = new Gravity((size, vector) => Vector2.Zero);

            Player = new Player(Content,
                                Content.Load<Texture2D>("Sprite/Rocket/Rocket"),
                                new Vector2(100, LevelSize.Height - 200),
                                this);

            Earth = new Sprite(Content,
                               Content.Load<Texture2D>("Sprite/Earth/Earth"),
                               new Vector2(LevelSize.Width - 300, 600),
                               this);

            HostileMobs = new List<Mob>()
            {
            };
        }
    }
}