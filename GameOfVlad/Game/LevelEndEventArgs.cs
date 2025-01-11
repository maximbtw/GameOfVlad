using System;

namespace GameOfVlad.Game;

public class LevelEndEventArgs : EventArgs
{
    public LevelEndReason Reason { get; init; }
}