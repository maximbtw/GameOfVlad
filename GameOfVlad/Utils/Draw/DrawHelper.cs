using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public static class DrawHelper
{
    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, Texture2D texture)
    {
        // Вычисляем длину линии
        float length = Vector2.Distance(start, end);

        // Вычисляем угол линии
        float angle = MathF.Atan2(end.Y - start.Y, end.X - start.X);

        // Рисуем линию как повернутый прямоугольник
        spriteBatch.Draw(
            texture,
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
}