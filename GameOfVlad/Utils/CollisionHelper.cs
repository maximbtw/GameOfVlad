using GameOfVlad.GameObjects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils;

public static class CollisionHelper
{
    // Проверяет пересечение двух коллайдеров
    public static bool CheckCollision(IColliderGameObject obj1, IColliderGameObject obj2)
    {
        // Получаем вершины
        Vector2[] obj1Corners = obj1.GetCorners();
        Vector2[] obj2Corners = obj2.GetCorners();

        // Проверяем оси для двух полигонов
        if (!HasSeparatingAxis(obj1Corners, obj2Corners) && !HasSeparatingAxis(obj2Corners, obj1Corners))
        {
            return true; 
        }

        return false;
    }

    private static bool HasSeparatingAxis(Vector2[] polygonObj1, Vector2[] polygonObj2)
    {
        for (int i = 0; i < polygonObj1.Length; i++)
        {
            // Находим нормаль к текущему ребру
            Vector2 edge = polygonObj1[(i + 1) % polygonObj1.Length] - polygonObj1[i];
            var axis = new Vector2(-edge.Y, edge.X);

            // Проецируем оба полигона на ось
            (float minA, float maxA) = ProjectPolygon(polygonObj1, axis);
            (float minB, float maxB) = ProjectPolygon(polygonObj2, axis);

            // Проверяем на разделяющую ось
            if (maxA < minB || maxB < minA)
            {
                return true; // Разделяющая ось найдена
            }
        }

        return false;
    }

    private static (float Min, float Max) ProjectPolygon(Vector2[] polygon, Vector2 axis)
    {
        float min = Vector2.Dot(polygon[0], axis);
        float max = min;

        for (int i = 1; i < polygon.Length; i++)
        {
            float projection = Vector2.Dot(polygon[i], axis);
            if (projection < min)
            {
                min = projection;
            }

            if (projection > max)
            {
                max = projection;
            }
        }

        return (min, max);
    }
}
