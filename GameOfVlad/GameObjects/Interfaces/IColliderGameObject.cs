using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Interfaces;

public interface IColliderGameObject : IGameObject
{
    /// <summary>
    /// Цвет для визуализации.
    /// </summary>
    public Color ColliderColor { get; set; }

    /// <summary>
    /// Возвращает вершины коллайдера с учетом поворота.
    /// </summary>
    Vector2[] GetCorners();
}