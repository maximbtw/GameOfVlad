using Microsoft.Xna.Framework;

namespace GameOfVlad.Entities.Interfaces;

public interface IPhysicalGameObject
{
    Vector2 Position { get; set; }
    
    /// <summary>
    /// Масса объекта
    /// </summary>
    float Mass { get; set; }        
    
    /// <summary>
    /// Максимальная скорость объекта
    /// </summary>
    float MaxVelocity { get; set; } 
    
    /// <summary>
    /// Скорость
    /// </summary>
    public Vector2 Velocity { get; set; }
}