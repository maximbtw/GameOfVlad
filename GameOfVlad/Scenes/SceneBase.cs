using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using GameOfVlad.GameRenderer.Handlers;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Mouse;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Scenes;

public abstract class SceneBase(ContentManager contentManager) : IDisposable
{
    private RendererObjectDispatcher _renderer;
    
    protected readonly ContentManager ContentManager = contentManager;
    
    protected KeyboardInputObserver KeyboardInputObserver;
    protected ICameraService CameraService => this.ContentManager.ServiceProvider.GetRequiredService<ICameraService>();
    protected IMouseService MouseService => this.ContentManager.ServiceProvider.GetRequiredService<IMouseService>();

    public void Load()
    {
        _renderer = new RendererObjectDispatcher();
        this.KeyboardInputObserver = new KeyboardInputObserver();

        RegisterRendererHandler(new MouseCursorRendererHandler(this.CameraService, this.MouseService));
        
        this.KeyboardInputObserver.KeyDown += e =>
        {
            if (e.Key == Keys.Q)
            {
                Settings.Debug = !Settings.Debug;
            }
        };

        InitRenderObjects();

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
        this.KeyboardInputObserver.Update();
        _renderer.Update(gameTime);

        UpdateCore(gameTime);
    }

    protected void AddRenderObject(params IRendererObject[] rendererObjects)
    {
        foreach (IRendererObject gameObject in rendererObjects)
        {
            _renderer.Add(gameObject);
        }
    }

    protected void RegisterRendererHandler(IRendererObjectHandler handler)
    {
        _renderer.RegisterHandler(handler);
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
    
    protected abstract IEnumerable<IRendererObject> InitRenderObjectsCore();
    
    private void InitRenderObjects()
    {
        foreach (IRendererObject obj in InitRenderObjectsCore())
        {
            _renderer.Add(obj);
        }
    }

    public void Dispose()
    {
        Unload();
    }
}