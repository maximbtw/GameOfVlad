using GameOfVlad.GameRenderer;
using GameOfVlad.Scenes.Game;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Game;

public interface ILevel : IRendererObject
{
    Rectangle LevelBounds { get; }
    
    LevelType LevelType { get; }
}