using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface IPhysicalGameObject : IGameObject
{
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