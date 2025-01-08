using GameOfVlad.Utils;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface IColliderGameObject : IGameGameObject
{
    /// <summary>
    /// Размер объекта (ширина и высота).
    /// </summary>
    public Size Size { get; } 
    
    /// <summary>
    /// Точка вращения относительно центра.
    /// </summary>
    public Vector2 Origin { get; }
    
    /// <summary>
    /// Угол поворота в радианах.
    /// </summary>
    public float Rotation { get; set; } 
    
    /// <summary>
    /// Скорость поворота.
    /// </summary>
    public float RotationVelocity { get; set; } 
    
    /// <summary>
    /// Цвет для визуализации.
    /// </summary>
    public Color ColliderColor { get; set; }

    /// <summary>
    /// Возвращает вершины коллайдера с учетом поворота.
    /// </summary>
    Vector2[] GetCorners();
    
    Vector2 RotatePoint(Vector2 point, Vector2 center, float angle);
}