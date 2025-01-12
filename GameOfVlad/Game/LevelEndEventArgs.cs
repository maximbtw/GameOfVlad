using System;

namespace GameOfVlad.Game;

public class LevelEndEventArgs : EventArgs
{
    public LevelEndReason Reason { get; init; }
    
    /// <summary>
    /// Возвращает или устанавливает игровое время уровня.
    /// </summary>
    public TimeSpan PlayTime { get; init; }
}