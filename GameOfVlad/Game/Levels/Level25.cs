﻿using System.Collections.Generic;
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
    public class Level25 : Level
    {
        public Level25(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
            LevelSize = new Size(Backgraund.Width, Backgraund.Height);
            _StateGame = StateGame.Space;
            StateProcess = State.Play;

            PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
            DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
            CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

            Name = "Level28";
            DeathCount = Game.DataManager.GetAllDeath(Name);
            IndexLevel = 28;

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