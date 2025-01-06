using System;
using GameOfVlad.Game;

namespace GameOfVlad.Scenes.Game;

public class GameLevelChangeEventArgs(ILevel oldLevel, ILevel newLevel) : EventArgs
{
    public ILevel OldLevel { get; } = oldLevel;
    
    public ILevel NewLevel { get; } = newLevel;
}