using System;
using System.Collections.Generic;
using GameOfVlad.Game.WeaponSystem;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameObjects.UI.Components.Game;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Audio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game;

public abstract class LevelBase(ContentManager contentManager) : IRendererObject
{
    public int UpdateOrder => 1;
    public Guid Guid { get; } = Guid.NewGuid();
    
    public event EventHandler<LevelEndEventArgs> OnLevelEnd;
    
    public bool Destroyed { get; set; }
    public virtual bool Loaded => _loaded;
    public bool IsActive { get; set; } = true;
    public bool Visible { get; set; } = true;
    public IRendererObject Parent
    {
        get => null;
        set => throw new NotSupportedException();
    }

    public IEnumerable<IRendererObject> Children 
    {
        get => [];
        set => throw new NotSupportedException();
    }

    protected readonly ContentManager ContentManager = contentManager;
    protected readonly IEffectDrawer EffectDrawer = new EffectDrawer();
    protected readonly IProjectileDrawer ProjectileDrawer = new ProjectileDrawer();

    protected IAudioService AudioService => this.ContentManager.ServiceProvider.GetRequiredService<IAudioService>();

    // TODO: Либо объеденить и добавить функционал в диспатчер.
    private RendererObjectDispatcher _renderer;
    private readonly List<IGameObject> _gameObjects = new();

    private bool _loaded;

    public void Load()
    {
        if (_loaded)
        {
            throw new Exception("Object is already loaded");
        }
        
        _renderer = new RendererObjectDispatcher();
        _renderer.Add(this.EffectDrawer);
        _renderer.Add(this.ProjectileDrawer);
        
        LoadCore();
        InitGameObjects();

        _loaded = true;
    }

    public void Unload()
    {
        UnloadCore();

        this.EffectDrawer.ClearEffects();
        this.ProjectileDrawer.ClearProjectiles();
        _gameObjects.ForEach(x => x.Destroy());
        _gameObjects.Clear();
        _renderer.Clear();

        _loaded = false;
    }
    
    public void Update(GameTime gameTime)
    {
        _renderer.Update(gameTime);
    }

    public void Destroy()
    {
        this.Destroyed = true;
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
            new HealthBar.Configuration(player.MaxHP, () => player.CurrentHP));

        return healthBar;
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