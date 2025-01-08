using System;
using GameOfVlad.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public class ColliderDrawer(ColliderEntity entity) : IDisposable
{
    // Статическая текстура 1x1 пиксель (общая для всех сущностей)
    private Texture2D _pixelTexture;

    public void DrawCollider(SpriteBatch spriteBatch)
    {
        if (_pixelTexture == null)
        {
            _pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _pixelTexture.SetData([Color.White]);
        }
        
        // Получаем вершины прямоугольника (коллайдера)
        Vector2 topLeft = entity.Position;
        Vector2 topRight = entity.Position + new Vector2(entity.Size.Width, 0);
        Vector2 bottomLeft = entity.Position + new Vector2(0, entity.Size.Height);
        Vector2 bottomRight = entity.Position + new Vector2(entity.Size.Width, entity.Size.Height);

        // Применяем вращение к каждой вершине относительно центра
        topLeft = RotatePoint(topLeft, entity.Position + entity.Origin, entity.Rotation);
        topRight = RotatePoint(topRight, entity.Position + entity.Origin, entity.Rotation);
        bottomLeft = RotatePoint(bottomLeft, entity.Position + entity.Origin, entity.Rotation);
        bottomRight = RotatePoint(bottomRight, entity.Position + entity.Origin, entity.Rotation);

        // Рисуем линии между вершинами
        DrawLine(spriteBatch, topLeft, topRight, entity.ColliderColor); // Верхняя линия
        DrawLine(spriteBatch, topRight, bottomRight, entity.ColliderColor); // Правая линия
        DrawLine(spriteBatch, bottomRight, bottomLeft, entity.ColliderColor); // Нижняя линия
        DrawLine(spriteBatch, bottomLeft, topLeft, entity.ColliderColor); // Левая линия
    }

    private Vector2 RotatePoint(Vector2 point, Vector2 center, float angle)
    {
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);

        // Смещаем точку к центру вращения
        Vector2 translatedPoint = point - center;

        // Применяем вращение
        float rotatedX = translatedPoint.X * cos - translatedPoint.Y * sin;
        float rotatedY = translatedPoint.X * sin + translatedPoint.Y * cos;

        // Возвращаем точку обратно
        return new Vector2(rotatedX, rotatedY) + center;
    }

    private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
        // Вычисляем длину линии
        float length = Vector2.Distance(start, end);

        // Вычисляем угол линии
        float angle = MathF.Atan2(end.Y - start.Y, end.X - start.X);

        // Рисуем линию как повернутый прямоугольник
        spriteBatch.Draw(
            _pixelTexture,
            start, // Начальная позиция линии
            null,
            color, // Цвет линии
            angle, // Угол наклона линии
            Vector2.Zero, // Точка вращения (верхний левый угол текстуры)
            new Vector2(length, 1), // Размер линии (длина и ширина 1 пиксель)
            SpriteEffects.None,
            0f
        );
    }

    public void Dispose()
    {
        _pixelTexture?.Dispose();
    }
}