using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes;

public abstract class SceneBase(IServiceProvider serviceProvider) : IDisposable
{
    private GameObjectRenderer _renderer;

    protected KeyboardInputObserver KeyboardInputObserver;

    protected readonly IServiceProvider ServiceProvider = serviceProvider;

    public void Load(ContentManager content)
    {
        _renderer = new GameObjectRenderer(content);
        this.KeyboardInputObserver = this.ServiceProvider.GetRequiredService<KeyboardInputObserver>();

        InitUiComponents(content);
        InitGameGameObjects(content);

        LoadCore(content);
    }

    public void Unload()
    {
        _renderer.Clear();

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

    protected virtual void LoadCore(ContentManager content)
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
    
    protected abstract IEnumerable<IGameGameObject> InitInitGameGameObjectsCore(ContentManager content);
    protected abstract IEnumerable<IUiComponent> InitUiComponentsCore(ContentManager content);
    
    private void InitGameGameObjects(ContentManager content)
    {
        foreach (IGameGameObject gameObject in InitInitGameGameObjectsCore(content))
        {
            _renderer.AddGameObject(gameObject);
        }
    }

    private void InitUiComponents(ContentManager content)
    {
        foreach (IUiComponent uiComponent in InitUiComponentsCore(content))
        {
            _renderer.AddGameObject(uiComponent);
        }
    }

    public void Dispose()
    {
        Unload();
    }
}