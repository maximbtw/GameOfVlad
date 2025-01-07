using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer;

public class Renderer(ContentManager contentManager)
{
    private readonly List<IGameObject> _gameObjects = new();
    private readonly List<IRendererModificator> _rendererModificators = new();

    public void AddModificator(IRendererModificator modificator)
    {
        _rendererModificators.Add(modificator);
    }

    public void ClearModificators()
    {
        _rendererModificators.Clear();
    }
    
    public void AddGameObject(IGameObject gameObject)
    {
        if (gameObject.Parent != null)
        {
            throw new InvalidOperationException("Cannot add game object to a child of a game object");
        }
        
        gameObject.Init(contentManager);
        
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
        foreach (IGameObject gameObject in _gameObjects.OrderBy(x => x.UpdateOrder))
        {
            UpdateGameObject(gameTime, gameObject);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (IGameObject gameObject in _gameObjects.OrderBy(e => e.DrawOrder))
        {
            DrawGameObject(gameTime, spriteBatch, gameObject);
        }
    }

    private void UpdateGameObject(GameTime gameTime, IGameObject gameObject)
    {
        if (!gameObject.IsActive)
        {
            return;
        }
        
        gameObject.Update(gameTime);
        foreach (IRendererModificator modificator in _rendererModificators)
        {
            modificator.Update(gameObject, gameTime);
        }

        if (gameObject.Children == null)
        {
            return;
        }
        
        foreach (IGameObject child in gameObject.Children.OrderBy(x => x.UpdateOrder))
        {
            UpdateGameObject(gameTime, child);
        }
    }
    
    private void DrawGameObject(GameTime gameTime, SpriteBatch spriteBatch, IGameObject gameObject)
    {
        if (!gameObject.IsActive)
        {
            return;
        }
        
        gameObject.Draw(gameTime, spriteBatch);
        foreach (IRendererModificator modificator in _rendererModificators)
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