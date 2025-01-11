using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities;
using GameOfVlad.GameObjects.UI.Components;
using GameOfVlad.GameObjects.UI.Effects;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.GameObjectRendererModificators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels;

public class Level1(ContentManager contentManager) : LevelBase(contentManager), ILevel
{
    public Rectangle LevelBounds => new(0, 0, 2000, 2000);
    public LevelType LevelType => LevelType.Level1;

    protected override void LoadCore()
    {
        RegisterRendererHandler(new PhysicRendererModificator
        {
            //Gravity = new Gravity(new Vector2(800, 800), 5000f)
        });
        
        RegisterRendererHandler(new LevelBorderRendererModificator(this.LevelBounds));
    }

    protected override IEnumerable<IGameObject> InitGameObjectsCore()
    {
        yield return new BackgroundGenerator(
            this.ContentManager.Load<Texture2D>("2025/Backgrounds/Game/Starfields/Starfield_04-512x512"), 
            this.LevelBounds);

        yield return new StarfallGenerator(this.ContentManager, this.LevelBounds);
        
        yield return new PlayerV2(this.ContentManager)
        {
            Texture = this.ContentManager.Load<Texture2D>("Sprite/Rocket/Rocket"),
            Position = new Vector2(100, 100),
            TrustPower = 10000,
            Mass = 100
        };
    }
    
    /*public Level1(GameOfVlad game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
    {
        Backgraund = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080");
        LevelSize = new Size(Backgraund.Width, Backgraund.Height);
        _StateGame = StateGame.Space;
        StateProcess = State.Play;

        PauseMenu = new PauseMenu(game, content, graphicsDevice, this);
        DeathMenu = new DeathMenu(game, content, graphicsDevice, this);
        CompliteMenu = new CompliteMenu(game, content, graphicsDevice, this);

        Name = "Level1";
        DeathCount = Game.DataManager.GetAllDeath(Name);
        IndexLevel = 1;

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
    }*/
}