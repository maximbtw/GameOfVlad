using System;
using System.Collections.Generic;
using System.Linq;
using GameOfVlad.GameObjects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.Physics;

public delegate Vector2 ForceDelegate(IPhysicalGameObject obj);

public static class Force
{
    /// <summary>
    /// Создаёт силу тяги, направленную вперёд, по направлению Rotation
    /// </summary>
    public static ForceDelegate CreateThrustForce(ITrustForcePhysicalGameObject trustForce)
    {
        return _ =>
        {
            // Направление тяги в виде нормализованного вектора
            var direction = new Vector2(
                (float)Math.Cos(trustForce.Rotation),
                (float)Math.Sin(trustForce.Rotation)
            );
            
            float force = trustForce.TrustPower;
            
            return direction * force;
        };
    }

    /// <summary>
    /// Создаёт силу гравитации, направленную к центру
    /// </summary>
    public static ForceDelegate CreateGravityForce(Vector2 gravityCenter, float gravityStrength)
    {
        return obj =>
        {
            Vector2 direction = gravityCenter - obj.Position;
            float distance = direction.Length();

            if (distance == 0)
            {
                return Vector2.Zero;
            }

            direction.Normalize();

            // Модифицированная формула с "+ offset" для сглаживания
            float offset = 0.002f; // Смещение для уменьшения влияния расстояния
            float force = gravityStrength / (distance * offset);
            
            return direction * force;
        };
    }

    /// <summary>
    /// Создаёт силу магнетизма, притягивающую или отталкивающую объект от указанного центра.
    /// </summary>
    public static ForceDelegate CreateMagneticForce(Vector2 magnetCenter, float magneticStrength)
    {
        return obj =>
        {
            Vector2 direction = magnetCenter - obj.Position; // Вектор от объекта к магниту
            float distance = direction.Length();

            // Избегаем деления на 0, если объект находится в точке магнита
            if (distance == 0)
            {
                return Vector2.Zero;
            }

            direction.Normalize(); // Приводим направление к единичному вектору
            float force = magneticStrength / (distance * distance); // Сила обратно пропорциональна квадрату расстояния
            return direction * force; // Итоговый вектор силы
        };
    }

    /// <summary>
    /// Комбинирует несколько сил
    /// </summary>
    public static ForceDelegate Combine(List<ForceDelegate> forces)
    {
        return obj =>
        {
            return forces.Aggregate(Vector2.Zero, (current, force) => current + force(obj));
        };
    }
}