using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game;

public abstract class LevelBase(ContentManager contentManager) : IRendererObject
{
    public int DrawOrder => (int)DrawOrderType.Player;
    public int UpdateOrder => 1;
    public Guid Guid => Guid.NewGuid();
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
    
    protected void RegisterRendererHandler(IRendererObjectHandler handler)
    {
        _renderer.RegisterHandler(handler);
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