using System;
using System.Collections.Generic;
using GameOfVlad.Game;
using GameOfVlad.Game.Levels;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Scenes.Game;

public class GameSceneStateManager(ContentManager contentManager)
{
    private readonly Dictionary<LevelType, Func<ContentManager, ILevel>> _levelTypeToLevelFabricIndex = new()
    {
        { LevelType.Level1 , x=> new Level1(x)}
    };
    
    private GameState _state = GameState.Loading;
    private ILevel _level = null!;
    
    public event EventHandler<GameStateChangeEventArgs> OnGameStateChanged;
    public event EventHandler<GameLevelChangeEventArgs> OnLevelChanged;
    
    public GameState GetState() => _state;
    
    public void SetLevel(LevelType levelType)
    {
        ILevel newLevel = _levelTypeToLevelFabricIndex[levelType].Invoke(contentManager);
        
        var eventArgs = new GameLevelChangeEventArgs(_level, newLevel);
        
        _level?.Unload();
        _level = newLevel;
        _level.Load();
        
        OnLevelChanged?.Invoke(this, eventArgs);
    }

    public void SetState(GameState state)
    {
        if (_state != state)
        {
            _level.GameStateChanged(state);
            
            OnGameStateChanged?.Invoke(this, new GameStateChangeEventArgs(state));
        }
        _state = state;
    }
}