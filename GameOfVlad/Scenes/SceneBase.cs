using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Tools.Keyboards;
using GameOfVlad.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Scenes;

public abstract class SceneBase<TCanvas> where TCanvas : ICanvas
{
    protected Renderer Renderer;
    private TCanvas _canvas;
    
    protected readonly IServiceProvider ServiceProvider;
    
    private readonly KeyboardStateObserver _keyboardStateObserver;
    
    protected IGraphicService GraphicService => ServiceProvider.GetRequiredService<IGraphicService>();

    public SceneBase(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        this._keyboardStateObserver = serviceProvider.GetRequiredService<KeyboardStateObserver>();
    }

    public void Init(ContentManager content)
    {
        Renderer = new Renderer(this.GraphicService.GetContentManager());
        _canvas = GetCanvas();

        InitBeginCore(content);
        
        _canvas.Init(content);

        foreach (UiComponent component in _canvas.GetComponents())
        {
            Renderer.AddGameObject(component);
        }

        BindKeyboardKeys(this._keyboardStateObserver);

        InitEndCore(content); 
    }

    public void Terminate()
    {
        Renderer.Clear();
        _canvas.Terminate();

        TerminalCore();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Renderer.Draw(gameTime, spriteBatch);
        
        DrawCore(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        Renderer.Update(gameTime);

        this._keyboardStateObserver.Update(gameTime);
        
        UpdateCore(gameTime);
    }

    protected void ClearScene()
    {
        Renderer.Clear();
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
    
    protected abstract TCanvas GetCanvas();
}