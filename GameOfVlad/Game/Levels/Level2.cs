using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Effects.Generators;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameRenderer.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels;

public class Level2(ContentManager contentManager) :  LevelBase(contentManager), ILevel
{
    public Rectangle LevelBounds => new(0, 0, 2000, 2000);
    public LevelType LevelType => LevelType.Level2;
    
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
            this.ContentManager,
            this.EffectDrawer,
            this.ContentManager.Load<Texture2D>("2025/Backgrounds/Game/Starfields/Starfield_05-512x512"), 
            this.LevelBounds);

        yield return new StarfallGenerator(this.ContentManager, this.EffectDrawer, this.LevelBounds);
        
        yield return new PlayerV2(this.ContentManager, this.EffectDrawer, this.ProjectileDrawer)
        {
            Texture = this.ContentManager.Load<Texture2D>("Sprite/Rocket/Rocket"),
            Position = new Vector2(500, 500),
            TrustPower = 10000,
            Mass = 100
        };
    }
    
    /*public Level2(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
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
    }*/
}