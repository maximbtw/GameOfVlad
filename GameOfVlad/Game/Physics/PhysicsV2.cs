using GameOfVlad.GameObjects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.Physics;

public class PhysicsV2
{
    public void ApplyForces(IPhysicalGameObject obj, ForceDelegate force, GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Рассчитываем суммарное ускорение
        Vector2 acceleration = force(obj) / obj.Mass;

        // Рассчитываем новую скорость
        Vector2 newVelocity = obj.Velocity + acceleration * deltaTime;

        // Ограничиваем скорость
        if (newVelocity.Length() > obj.MaxVelocity)
        {
            newVelocity.Normalize();
            newVelocity *= obj.MaxVelocity;
        }

        // Обновляем текущую скорость
        obj.Velocity = newVelocity;
        
        // Обновляем позицию объекта
        obj.Position += obj.Velocity * deltaTime;
    }
}