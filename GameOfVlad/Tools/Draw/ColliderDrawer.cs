using System;
using GameOfVlad.Entities;
using GameOfVlad.Services.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Tools.Draw;

public class ColliderDrawer : IDisposable
{
    // Статическая текстура 1x1 пиксель (общая для всех сущностей)
    private readonly Texture2D _pixelTexture;
    private readonly ColliderEntity _entity;

    public ColliderDrawer(ColliderEntity entity, IGraphicService graphicService)
    {
        _entity = entity;
        
        GraphicsDeviceManager graphicsDeviceManager = graphicService.GetGraphicsDeviceManager();

        _pixelTexture = new Texture2D(graphicsDeviceManager.GraphicsDevice, 1, 1);
        _pixelTexture.SetData([Color.White]);
    }

    public void DrawCollider(SpriteBatch spriteBatch)
    {
        // Получаем вершины прямоугольника (коллайдера)
        Vector2 topLeft = _entity.Position;
        Vector2 topRight = _entity.Position + new Vector2(_entity.Size.Width, 0);
        Vector2 bottomLeft = _entity.Position + new Vector2(0, _entity.Size.Height);
        Vector2 bottomRight = _entity.Position + new Vector2(_entity.Size.Width, _entity.Size.Height);

        // Применяем вращение к каждой вершине относительно центра
        topLeft = RotatePoint(topLeft, _entity.Position + _entity.Origin, _entity.Rotation);
        topRight = RotatePoint(topRight, _entity.Position + _entity.Origin, _entity.Rotation);
        bottomLeft = RotatePoint(bottomLeft, _entity.Position + _entity.Origin, _entity.Rotation);
        bottomRight = RotatePoint(bottomRight, _entity.Position + _entity.Origin, _entity.Rotation);

        // Рисуем линии между вершинами
        DrawLine(spriteBatch, topLeft, topRight, _entity.ColliderColor); // Верхняя линия
        DrawLine(spriteBatch, topRight, bottomRight, _entity.ColliderColor); // Правая линия
        DrawLine(spriteBatch, bottomRight, bottomLeft, _entity.ColliderColor); // Нижняя линия
        DrawLine(spriteBatch, bottomLeft, topLeft, _entity.ColliderColor); // Левая линия
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