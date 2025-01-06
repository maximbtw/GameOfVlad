using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game;

public abstract class LevelBase(IServiceProvider serviceProvider)
{
    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    
    protected readonly List<IGameObject> GameObjects = new();
    private ILevelCanvas Canvas { get; set; }
    
    public void Init(ContentManager content)
    {
        this.Canvas = CreateCanvas();
        this.Canvas.Init(content);

        foreach (IGameObject gameObject in InitGameObjects(content))
        {
            this.GameObjects.Add(gameObject);
        }
        
        InitCore(content);
    }

    public void Terminate()
    {
        this.GameObjects.Clear();
        
        TerminateCore();
    }

    public IEnumerable<IGameObject> GetGameObjects() => GameObjects;

    public void Play()
    {
        this.GameObjects.ForEach(x => x.IsActive = true);
    }

    public void Stop()
    {
        this.GameObjects.ForEach(x => x.IsActive = false);
    }

    public ILevelCanvas GetCanvas() => this.Canvas;
    
    public abstract ILevelCanvas CreateCanvas();

    protected abstract IEnumerable<IGameObject> InitGameObjects(ContentManager content);

    protected virtual void InitCore(ContentManager content)
    {
        
    }

    protected virtual void TerminateCore()
    {
        
    }
}