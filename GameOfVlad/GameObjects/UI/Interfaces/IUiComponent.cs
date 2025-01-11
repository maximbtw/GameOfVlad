using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.UI.Interfaces;

public interface IUiComponent : IGameObject
{
    Vector2 PositionByCamera { get; }
}