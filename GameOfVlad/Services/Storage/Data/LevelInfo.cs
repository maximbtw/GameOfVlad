using System;
using GameOfVlad.Game;

namespace GameOfVlad.Services.Storage.Data;

public class LevelInfo
{
    public LevelType LevelType { get; set; }
    
    public bool Completed { get; set; }
    
    public TimeSpan? BestTime { get; set; }
    
    public int RetriesNumber { get; set; }
}