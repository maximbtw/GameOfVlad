using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities;
using GameOfVlad.GameObjects.UI.Components.Game;
using GameOfVlad.GameRenderer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game;

public abstract class LevelBase(ContentManager contentManager) : IRendererObject
{
    public int DrawOrder => (int)DrawOrderType.Player;
    public int UpdateOrder => 1;
    public Guid Guid => Guid.NewGuid();
    
    public event EventHandler<LevelEndEventArgs> OnLevelEnd;
    
    public bool Destroyed { get; set; } = false;
    public virtual bool Loaded => _loaded;
    public bool IsActive { get; set; } = true;
    public bool Visible { get; set; } = true;
    public IRendererObject Parent
    {
        get => null;
        set => throw new NotSupportedException();
    }

    public IEnumerable<IRendererObject> ChildrenBefore 
    {
        get => [];
        set => throw new NotSupportedException();
    }
    
    public IEnumerable<IRendererObject> ChildrenAfter
    {
        get => [];
        set => throw new NotSupportedException();
    }

    protected readonly ContentManager ContentManager = contentManager;

    protected IGraphicsDeviceService GraphicsDeviceService =>
        this.ContentManager.ServiceProvider.GetRequiredService<IGraphicsDeviceService>();

    // TODO: Либо объеденить и добавить функционал в диспатчер.
    private RendererObjectDispatcher _renderer;
    private readonly List<IGameObject> _gameObjects = new();

    private bool _loaded;

    public IRendererObject GetParent() => null;

    public IEnumerable<IRendererObject> GetChildren() => _gameObjects;

    public void Load()
    {
        if (_loaded)
        {
            throw new Exception("Object is already loaded");
        }
        
        _renderer = new RendererObjectDispatcher();
        
        InitGameObjects();
        LoadCore();

        _loaded = true;
    }

    public void Unload()
    {
        UnloadCore();

        _gameObjects.ForEach(x => x.Destroyed = true);
        _gameObjects.Clear();
        _renderer.Clear();

        _loaded = false;
    }
    
    public void Update(GameTime gameTime)
    {
        _renderer.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _renderer.Draw(gameTime, spriteBatch);
    }
    
    protected virtual void LoadCore()
    {
    }

    protected virtual void UnloadCore()
    {
    }

    protected void OnLevelCompleted()
    {
        // TODO: Timer
        OnLevelEnd?.Invoke(this,
            new LevelEndEventArgs { Reason = LevelEndReason.Completed, PlayTime = TimeSpan.MaxValue });
    }
    
    protected void OnPlayerDead()
    {
        // TODO: Timer
        OnLevelEnd?.Invoke(this,
            new LevelEndEventArgs { Reason = LevelEndReason.PlayerDead, PlayTime = TimeSpan.MaxValue });
    }
    
    protected void RegisterRendererHandlers(params IRendererObjectHandler[] handlers)
    {
        foreach (IRendererObjectHandler handler in handlers)
        {
            _renderer.RegisterHandler(handler);
        }
    }

    protected HealthBar CreateHealthBar(PlayerV2 player)
    {
        var healthBar = new HealthBar(this.ContentManager,
            new HealthBar.Configuration(player.MaxHP, () => player.CurrentHP))
        {
            Position = CalculateHealthBarPosition(player)
        };

        return healthBar;
    }
    
    //TODO: поправить после реализации разрешения экрана.
    private Vector2 CalculateHealthBarPosition(PlayerV2 player)
    {
        GraphicsDevice graphicsDevice = GraphicsDeviceService.GraphicsDevice;
        
        // Получаем размеры экрана
        int screenWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
        int screenHeight = graphicsDevice.PresentationParameters.BackBufferHeight;

        // Количество строк и колонок
        int totalHearts = player.MaxHP / HealthBar.HeartEachHp;
        int rows = (int)Math.Ceiling((float)totalHearts / HealthBar.MaxHeartsInRow);
        int columns = Math.Min(totalHearts, HealthBar.MaxHeartsInRow);

        // Ширина и высота HealthBar
        int healthBarWidth = columns * (32 + HealthBar.HeartSpacing) - HealthBar.HeartSpacing;
        int healthBarHeight = rows * (32 + HealthBar.HeartSpacing) - HealthBar.HeartSpacing;
        
        return new Vector2(screenWidth - healthBarWidth - 10 - 300, 10); 
    }
    
    protected abstract IEnumerable<IGameObject> InitGameObjectsCore();
    
    private void InitGameObjects()
    {
        foreach (IGameObject obj in InitGameObjectsCore())
        {
            _renderer.Add(obj);
            _gameObjects.Add(obj);
        }
    }
}