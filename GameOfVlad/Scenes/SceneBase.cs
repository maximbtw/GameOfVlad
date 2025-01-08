using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes;

public abstract class SceneBase(IServiceProvider serviceProvider) : IDisposable
{
    private readonly KeyboardStateObserver _keyboardStateObserver = serviceProvider.GetRequiredService<KeyboardStateObserver>();
    private Renderer _renderer;

    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    protected IGraphicService GraphicService => ServiceProvider.GetRequiredService<IGraphicService>();

    public void Init(ContentManager content)
    {
        _renderer = new Renderer(this.GraphicService.GetContentManager());
        
        InitBeginCore(content);

        InitUiComponents(content);
        InitGameGameObjects(content);
        
        BindKeyboardKeys(_keyboardStateObserver);

        InitEndCore(content);
    }

    public void Terminate()
    {
        _renderer.Clear();

        TerminalCore();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _renderer.Draw(gameTime, spriteBatch);

        DrawCore(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        _renderer.Update(gameTime);

        _keyboardStateObserver.Update(gameTime);

        UpdateCore(gameTime);
    }

    protected void AddGameObjects(IEnumerable<IGameObject> gameObjects)
    {
        foreach (IGameObject gameObject in gameObjects)
        {
            _renderer.AddGameObject(gameObject);
        }
    }
    
    protected void AddRendererModificators(IEnumerable<IRendererModificator> modificators)
    {
        foreach (IRendererModificator modificator in modificators)
        {
            _renderer.AddModificator(modificator);
        }
    }
    
    protected void ClearModificators()
    {
        _renderer.ClearModificators();
    }
    
    protected virtual void InitBeginCore(ContentManager content)
    {
    }

    protected virtual void InitEndCore(ContentManager content)
    {
    }

    protected virtual void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    protected virtual void UpdateCore(GameTime gameTime)
    {
    }

    protected virtual void TerminalCore()
    {
    }

    protected abstract void BindKeyboardKeys(KeyboardStateObserver keyboard);
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
        Terminate();
    }
}