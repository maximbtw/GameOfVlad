using System;
using System.Collections.Generic;
using System.Linq;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.Scenes.Game;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game;

public abstract class LevelBase(IServiceProvider serviceProvider)
{
    protected readonly IServiceProvider ServiceProvider = serviceProvider;
    
    private readonly List<IGameObject> _alwaysVisiblyGameObjects = new();
    private readonly List<IGameObject> _gameObjects = new();

    public IEnumerable<IGameObject> GetGameObjects() => _gameObjects.Concat(_alwaysVisiblyGameObjects);
    
    public void Load(ContentManager content)
    {
        foreach (IGameGameObject gameObject in LoadGameGameObjects(content))
        {
            _gameObjects.Add(gameObject);
        }
        
        foreach (IUiComponent gameObject in LoadUiComponents(content))
        {
            _gameObjects.Add(gameObject);
        }
        
        foreach (IGameGameObject gameObject in LoadAlwaysVisiblyGameGameObjects(content))
        {
            _alwaysVisiblyGameObjects.Add(gameObject);
        }
        
        foreach (IUiComponent gameObject in LoadAlwaysVisiblyUiComponents(content))
        {
            _alwaysVisiblyGameObjects.Add(gameObject);
        }
    }

    public void Unload()
    {
        _gameObjects.ForEach(x=>x.Destroyed = true);
        _gameObjects.Clear();
    }

    public void GameStateChanged(GameState state)
    {
        if (state == GameState.Play)
        {
            _gameObjects.ForEach(x => x.IsActive = true);
        }
        else if (state == GameState.Pause)
        {
            _gameObjects.ForEach(x => x.IsActive = false);
        }
    }
    
    protected abstract IEnumerable<IUiComponent> LoadAlwaysVisiblyUiComponents(ContentManager content);
    protected abstract IEnumerable<IGameGameObject> LoadAlwaysVisiblyGameGameObjects(ContentManager content);
    protected abstract IEnumerable<IUiComponent> LoadUiComponents(ContentManager content);
    protected abstract IEnumerable<IGameGameObject> LoadGameGameObjects(ContentManager content);
}