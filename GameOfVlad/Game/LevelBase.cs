using System.Collections.Generic;
using System.Linq;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.Scenes.Game;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game;

public abstract class LevelBase(ContentManager contentManager)
{
    protected readonly ContentManager ContentManager = contentManager;
    
    private readonly List<IGameObject> _alwaysVisiblyGameObjects = new();
    private readonly List<IGameObject> _gameObjects = new();

    public IEnumerable<IGameObject> GetGameObjects() => _gameObjects.Concat(_alwaysVisiblyGameObjects);
    
    public void Load()
    {
        foreach (IGameGameObject gameObject in LoadGameGameObjects())
        {
            _gameObjects.Add(gameObject);
        }
        
        foreach (IUiComponent gameObject in LoadUiComponents())
        {
            _gameObjects.Add(gameObject);
        }
        
        foreach (IGameGameObject gameObject in LoadAlwaysVisiblyGameGameObjects())
        {
            _alwaysVisiblyGameObjects.Add(gameObject);
        }
        
        foreach (IUiComponent gameObject in LoadAlwaysVisiblyUiComponents())
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
    
    protected abstract IEnumerable<IUiComponent> LoadAlwaysVisiblyUiComponents();
    protected abstract IEnumerable<IGameGameObject> LoadAlwaysVisiblyGameGameObjects();
    protected abstract IEnumerable<IUiComponent> LoadUiComponents();
    protected abstract IEnumerable<IGameGameObject> LoadGameGameObjects();
}