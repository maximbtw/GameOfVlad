using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.Physics;

public interface IPhysicalGameObject
{
    Vector2 Position { get; set; }
    
    float Rotation { get; set; }
    
    /// <summary>
    /// Сила тяги
    /// </summary>
    float ThrustPower { get; set; }
    
    /// <summary>
    /// Масса объекта
    /// </summary>
    float Mass { get; }          
    
    /// <summary>
    /// Максимальная скорость объекта
    /// </summary>
    float MaxVelocity { get; }  
}