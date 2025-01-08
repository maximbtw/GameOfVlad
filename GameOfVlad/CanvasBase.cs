using System;
using System.Collections.Generic;
using GameOfVlad.Services.Graphic;
using GameOfVlad.UI;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad;

public abstract class CanvasBase(IServiceProvider serviceProvider)
{
    protected readonly List<UiComponent> UiComponents = new();
    protected readonly IServiceProvider ServiceProvider = serviceProvider;

    public IEnumerable<UiComponent> GetComponents() => this.UiComponents;
    
    public void Init(ContentManager content)
    {
        foreach (UiComponent uiComponent in GetUiComponents(content))
        { 
            this.UiComponents.Add(uiComponent);
        }

        InitCore(content);
    }

    public void Terminate()
    {
        this.UiComponents.Clear();

        TerminateCore();
    }

    protected virtual void InitCore(ContentManager content)
    {
    }

    protected virtual void TerminateCore()
    {
    }

    
    protected abstract IEnumerable<UiComponent> GetUiComponents(ContentManager content);
}