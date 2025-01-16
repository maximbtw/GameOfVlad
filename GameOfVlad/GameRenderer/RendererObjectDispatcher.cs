using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public class RendererObjectDispatcher
{
    private readonly List<IRendererObject> _gameObjects = new();
    private readonly List<IRendererObjectHandler> _handlers = new();
    
    public void Add(IRendererObject gameObject)
    {
        if (gameObject.Parent != null)
        {
            throw new InvalidOperationException("Cannot add game object to a child of a game object");
        }
        
        gameObject.Load();
        
        _gameObjects.Add(gameObject);
    }
    
    public void Remove(IRendererObject gameObject)
    {
        gameObject.Unload();
        
        _gameObjects.Remove(gameObject);
    }

    public void Clear()
    {
        foreach (IRendererObject gameObject in _gameObjects)
        {
            gameObject.Unload();
        }
        
        _gameObjects.Clear();
    }
    
    public void RegisterHandler(IRendererObjectHandler handler)
    {
        _handlers.Add(handler);
    }
    
    public void Update(GameTime gameTime)
    {
        var destroy = new List<IRendererObject>();
        foreach (IRendererObject gameObject in _gameObjects.OrderBy(x => x.UpdateOrder))
        {
            UpdateGameObject(gameTime, gameObject, ref destroy);
        }

        foreach (IRendererObject gameObject in destroy)
        {
            Remove(gameObject);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (IRendererObject gameObject in _gameObjects)
        {
            DrawGameObject(gameTime, spriteBatch, gameObject);
        }
    }

    private void UpdateGameObject(GameTime gameTime, IRendererObject gameObject, ref List<IRendererObject> destroy)
    {
        if (gameObject.Destroyed)
        {
            destroy.Add(gameObject);
            
            return;
        }
        
        if (!gameObject.IsActive)
        {
            return;
        }

        // Если объект не загружен, нельзя его обновлять.
        if (!gameObject.Loaded)
        {
            gameObject.Load();
        }
        
        gameObject.Update(gameTime);
        UpdateByHandler(gameTime, gameObject);
        
        foreach (IRendererObject child in gameObject.Children.OrderBy(x => x.UpdateOrder))
        {
            UpdateGameObject(gameTime, child, ref destroy);
        }
    }
    
    private void DrawGameObject(GameTime gameTime, SpriteBatch spriteBatch, IRendererObject gameObject)
    {
        if (!gameObject.Visible)
        {
            return;
        }
        
        gameObject.Draw(gameTime, spriteBatch);
        DrawByHandler(gameTime, spriteBatch, gameObject);
        
        foreach (IRendererObject child in gameObject.Children)
        {
            DrawGameObject(gameTime, spriteBatch, child);
        }
    }
    
    private void UpdateByHandler(GameTime gameTime, IRendererObject obj)
    {
        foreach (IRendererObjectHandler handler in _handlers)
        {
            handler.Update(gameTime, obj, _gameObjects);
        }
    }
    
    private void DrawByHandler(GameTime gameTime, SpriteBatch spriteBatch, IRendererObject obj)
    {
        foreach (IRendererObjectHandler handler in _handlers)
        {
            handler.Draw(gameTime, spriteBatch, obj);
        }
    }
}