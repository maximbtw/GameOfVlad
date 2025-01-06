using System;
using GameOfVlad.Game;
using GameOfVlad.Services.Graphic;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Scenes.Game;

public class GameSceneStateManager(IGraphicService graphicService)
{
    private GameState _state = GameState.Loading;
    private ILevel _level = null!;
    
    public event EventHandler<GameStateChangeEventArgs> OnGameStateChanged;
    public event EventHandler<GameLevelChangeEventArgs> OnLevelChanged;
    
    public GameState GetState() => _state;
    
    public ILevel GetLevel() => _level;
    
    public void SetLevel(ILevel newLevel)
    {
        ContentManager content = graphicService.GetContentManager();

        var eventArgs = new GameLevelChangeEventArgs(_level, newLevel);
        
        _level?.Terminate();
        _level = newLevel;
        _level.Init(content);
        
        OnLevelChanged?.Invoke(this, eventArgs);
    }

    public void SetState(GameState state)
    {
        if (_state != state)
        {
            OnGameStateChanged?.Invoke(this, new GameStateChangeEventArgs(state));
        }
        _state = state;
    }
}