using System;
using System.Collections.Generic;
using System.Linq;
using GameOfVlad.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public class GameObjectRenderer
{
    private readonly List<IGameObject> _gameObjects = new();
    private readonly List<IGameObjectRendererModificator> _rendererModificators = new();

    public void AddModificator(IGameObjectRendererModificator modificator)
    {
        _rendererModificators.Add(modificator);
    }

    public void RemoveModificators(IGameObjectRendererModificator modificator)
    {
        _rendererModificators.Remove(modificator);
    }
    
    public void ResetModificators()
    {
        _rendererModificators.Clear();
    }
    
    public void AddGameObject(IGameObject gameObject)
    {
        if (gameObject.Parent != null)
        {
            throw new InvalidOperationException("Cannot add game object to a child of a game object");
        }
        
        gameObject.Init();
        
        _gameObjects.Add(gameObject);
    }
    
    public void RemoveGameObject(IGameObject gameObject)
    {
        gameObject.Terminate();
        
        _gameObjects.Remove(gameObject);
    }

    public void Clear()
    {
        foreach (IGameObject gameObject in _gameObjects)
        {
            gameObject.Terminate();
        }
        
        _gameObjects.Clear();
    }
    
    public void Update(GameTime gameTime)
    {
        var destroy = new List<IGameObject>();
        foreach (IGameObject gameObject in _gameObjects.OrderBy(x => x.UpdateOrder))
        {
            UpdateGameObject(gameTime, gameObject, ref destroy);
        }

        foreach (IGameObject gameObject in destroy)
        {
            RemoveGameObject(gameObject);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (IGameObject gameObject in _gameObjects.OrderBy(e => e.DrawOrder))
        {
            DrawGameObject(gameTime, spriteBatch, gameObject);
        }
    }

    private void UpdateGameObject(GameTime gameTime, IGameObject gameObject, ref List<IGameObject> destroy)
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
        
        gameObject.Update(gameTime);
        foreach (IGameObjectRendererModificator modificator in _rendererModificators)
        {
            modificator.Update(gameObject, gameTime);
        }

        if (gameObject.Children == null)
        {
            return;
        }
        
        foreach (IGameObject child in gameObject.Children.OrderBy(x => x.UpdateOrder))
        {
            UpdateGameObject(gameTime, child, ref destroy);
        }
    }
    
    private void DrawGameObject(GameTime gameTime, SpriteBatch spriteBatch, IGameObject gameObject)
    {
        //TODO: сценарии когда объект не активен, но нужно отрисовать
        if (!gameObject.IsActive)
        {
            return;
        }
        
        gameObject.Draw(gameTime, spriteBatch);
        foreach (IGameObjectRendererModificator modificator in _rendererModificators)
        {
            modificator.Draw(gameObject, gameTime, spriteBatch);
        }
        
        if (gameObject.Children == null)
        {
            return;
        }
        
        foreach (IGameObject child in gameObject.Children.OrderBy(x => x.UpdateOrder))
        {
            DrawGameObject(gameTime, spriteBatch, child);
        }
    }
}