using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects;

public abstract class DrawerGameObject
{
    public bool Loaded { get; private set; }
    public Guid Guid { get; } = Guid.NewGuid();
    public bool Destroyed { get; set; }
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
        get => _gameObjects;
        set => throw new NotSupportedException();
    }

    private readonly List<IGameObject> _gameObjects = new();

    public void Load()
    {
        if (this.Loaded)
        {
            throw new Exception("Object is already loaded");
        }
        
        this.Loaded = true;
    }

    public void Unload()
    {
        this.Loaded = false;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public void Update(GameTime gameTime)
    {
        _gameObjects.RemoveAll(x => x.Destroyed);
    }

    public void Destroy()
    {
        this.Destroyed = true;
    }

    protected void AddGameObject(IGameObject gameObject)
    {
        gameObject.Load();
        _gameObjects.Add(gameObject);
    }
    
    protected void Clear()
    {
        _gameObjects.ForEach(x => x.Destroy());
        _gameObjects.Clear();
    }
}