using System;
using System.Collections.Generic;
using GameOfVlad.Game;
using GameOfVlad.Game.Levels;
using GameOfVlad.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Scenes.Game;

public class GameSceneStateManager(ContentManager contentManager)
{
    private readonly Dictionary<LevelType, Func<ContentManager, ILevel>> _levelTypeToLevelFabricIndex = new()
    {
        { LevelType.Level1 , x=> new Level1(x)},
        { LevelType.Level2 , x=> new Level2(x)}
    };
    
    private GameState _state = GameState.Loading;
    private ILevel _level = null!;
    
    public event EventHandler<GameStateChangeEventArgs> OnGameStateChanged;
    public event EventHandler<GameLevelChangeEventArgs> OnLevelChanged;
    
    public GameState GetState() => _state;
    
    public LevelType GetCurrentLevelType() => _level.LevelType;

    public LevelType GetNextLevelType() => _level.LevelType + 1;
    
    public void SetLevel(LevelType levelType)
    {
        ILevel newLevel = _levelTypeToLevelFabricIndex[levelType].Invoke(contentManager);
        
        var eventArgs = new GameLevelChangeEventArgs(_level, newLevel);

        newLevel.OnLevelEnd += OnLevelEnd;
        _level = newLevel;

        SetState(GameState.Play);
        OnLevelChanged?.Invoke(this, eventArgs);
    }

    public void SetState(GameState state)
    {
        if (_state != state)
        {
            _level.IsActive = state == GameState.Play;
            
            OnGameStateChanged?.Invoke(this, new GameStateChangeEventArgs(state));
        }
        _state = state;
    }
    
    private void OnLevelEnd(object sender, LevelEndEventArgs e)
    {
        switch (e.Reason)
        {
            case LevelEndReason.Completed:
                var storageService = contentManager.ServiceProvider.GetRequiredService<IStorageService>();
                
                storageService.CompleteLevel(_level.LevelType, (TimeSpan)e.PlayTime!);
                
                SetState(GameState.LevelCompleted);
                break;
            case LevelEndReason.PlayerDead:
                SetState(GameState.PlayerDead);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}