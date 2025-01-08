using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface IPhysicalGameObject : IGameGameObject
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