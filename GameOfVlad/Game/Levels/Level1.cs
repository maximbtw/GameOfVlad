﻿using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities;
using GameOfVlad.GameObjects.UI.Effects;
using GameOfVlad.GameRenderer.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels;

public class Level1(ContentManager contentManager) : LevelBase(contentManager), ILevel
{
    public Rectangle LevelBounds => new(0, 0, 2000, 2000);
    public LevelType LevelType => LevelType.Level1;
    public event EventHandler<LevelEndEventArgs> OnLevelEnd;

    protected override void LoadCore()
    {
        RegisterRendererHandlers(
            new PhysicRendererHandler(),
            new LevelBorderRendererHandler(this.LevelBounds),
            new GameObjectCollisionRendererHandler()
        );
    }

    protected override IEnumerable<IGameObject> InitGameObjectsCore()
    {
        yield return new BackgroundGenerator(
            this.ContentManager.Load<Texture2D>("2025/Backgrounds/Game/Starfields/Starfield_04-512x512"), 
            this.LevelBounds);

        yield return new StarfallGenerator(this.ContentManager, this.LevelBounds);
        
        var planet = new Planet
        {
            Texture = this.ContentManager.Load<Texture2D>("Sprite/Earth/Earth"),
            Position = new Vector2(1000, 1000),
        };

        planet.OnPlayerCollisionWithPlanet += () =>
            OnLevelEnd?.Invoke(this, new LevelEndEventArgs { Reason = LevelEndReason.Completed });

        yield return planet;
        
        yield return new PlayerV2(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Sprite/Rocket/Rocket"),
            Position = new Vector2(100, 100),
            TrustPower = 10000,
            Mass = 100
        };
    }
}