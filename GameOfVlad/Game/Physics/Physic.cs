using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.Physics;

public class Physic
{
    private Vector2 _currentVelocity = Vector2.Zero;

    // Применение физики и сил к объекту
    public void ApplyForces(IPhysicalGameObject obj, ForceDelegate forces, GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Рассчитываем суммарное ускорение
        Vector2 acceleration = forces(obj) / obj.Mass;

        // Рассчитываем новую скорость
        Vector2 newVelocity = _currentVelocity + acceleration * deltaTime;

        // Ограничиваем скорость
        if (newVelocity.Length() > obj.MaxVelocity)
        {
            newVelocity.Normalize();
            newVelocity *= obj.MaxVelocity;
        }

        // Обновляем текущую скорость
        this._currentVelocity = newVelocity;

        // Обновляем позицию объекта
        obj.Position += _currentVelocity * deltaTime;
    }
}