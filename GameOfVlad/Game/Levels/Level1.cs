using System;
using System.Collections.Generic;
using GameOfVlad.Entities;
using GameOfVlad.Game.Physics;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.Modificators;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Tools.Keyboards;
using GameOfVlad.UI;
using GameOfVlad.UI.Background;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.Levels;


public class Level1(IServiceProvider serviceProvider) : LevelBase(serviceProvider), ILevel
{
    public LevelType LevelType => LevelType.Level1;
    
    public IEnumerable<IRendererModificator> GetLevelModificators()
    {
        yield return new PhysicRendererModificator(new Physic());
    }

    private IGraphicService GraphicService => this.ServiceProvider.GetRequiredService<IGraphicService>();
    
    public override ILevelCanvas CreateCanvas() => new CanvasLevel1(GraphicService);
    
    protected override IEnumerable<IGameObject> InitGameObjects(ContentManager content)
    {
        yield return new PlayerV2(this.ServiceProvider)
        {
            Texture = content.Load<Texture2D>("Sprite/Rocket/Rocket"),
            Position = new Vector2(100, 100)
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

public class CanvasLevel1(IGraphicService graphicService) : LevelCanvas(graphicService), ILevelCanvas
{
    protected override IEnumerable<UiComponent> GetUiComponents(ContentManager content)
    {
        yield return new Background
        {
            Texture = content.Load<Texture2D>("Pages/GamePlay/Backgraund1080"),
            Size = new Vector2(1920, 1080)
        };
        
        yield break;
    }

    protected override IEnumerable<UiComponent> GetHideUiComponents(ContentManager content)
    {
        yield break;
    }
}