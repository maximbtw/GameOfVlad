using GameOfVlad.Utils;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Entities.Interfaces;

public interface IColliderGameObject
{
    /// <summary>
    /// Позиция объекта.
    /// </summary>
    public Vector2 Position { get; set; } 
    
    /// <summary>
    /// Размер объекта (ширина и высота).
    /// </summary>
    public Size Size { get; set; } 
    
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