using System;
using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Game;

public interface ILevel : IRendererObject
{
    Rectangle LevelBounds { get; }
    
    LevelType LevelType { get; }
    
    event EventHandler<LevelEndEventArgs> OnLevelEnd;
}