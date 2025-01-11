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
    
    /// <summary>
    /// Проверяет, пересекается ли данный объект с другим.
    /// </summary>
    bool Intersects(IColliderGameObject other);

    void OnCollision(IColliderGameObject other);
    
    void OnCollisionEnter(IColliderGameObject other);
    
    void OnCollisionExit(IColliderGameObject other);
}