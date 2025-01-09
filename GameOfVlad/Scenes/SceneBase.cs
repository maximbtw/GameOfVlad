using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes;

public abstract class SceneBase(ContentManager contentManager) : IDisposable
{
    private GameObjectRenderer _renderer;

    protected KeyboardInputObserver KeyboardInputObserver;
    
    protected readonly ContentManager ContentManager = contentManager;

    public void Load()
    {
        _renderer = new GameObjectRenderer();
        this.KeyboardInputObserver = new KeyboardInputObserver();

        InitUiComponents();
        InitGameGameObjects();

        LoadCore();
    }

    public void Unload()
    {
        _renderer.Clear();
        this.ContentManager.Unload();

        UnloadCore();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _renderer.Draw(gameTime, spriteBatch);

        DrawCore(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        KeyboardInputObserver.Update();
        _renderer.Update(gameTime);

        UpdateCore(gameTime);
    }

    protected void AddGameObjects(IEnumerable<IGameObject> gameObjects)
    {
        foreach (IGameObject gameObject in gameObjects)
        {
            _renderer.AddGameObject(gameObject);
        }
    }
    
    protected void AddRendererModificators(IEnumerable<IGameObjectRendererModificator> modificators)
    {
        foreach (IGameObjectRendererModificator modificator in modificators)
        {
            _renderer.AddModificator(modificator);
        }
    }
    
    protected void ResetModificators()
    {
        _renderer.ResetModificators();
    }

    protected virtual void LoadCore()
    {
    }

    protected virtual void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    protected virtual void UpdateCore(GameTime gameTime)
    {
    }

    protected virtual void UnloadCore()
    {
    }
    
    protected abstract IEnumerable<IGameGameObject> InitInitGameGameObjectsCore();
    protected abstract IEnumerable<IUiComponent> InitUiComponentsCore();
    
    private void InitGameGameObjects()
    {
        foreach (IGameGameObject gameObject in InitInitGameGameObjectsCore())
        {
            _renderer.AddGameObject(gameObject);
        }
    }

    private void InitUiComponents()
    {
        foreach (IUiComponent uiComponent in InitUiComponentsCore())
        {
            _renderer.AddGameObject(uiComponent);
        }
    }

    public void Dispose()
    {
        Unload();
    }
}