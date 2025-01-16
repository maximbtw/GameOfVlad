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
        
        spriteBatch.Draw(
            texture,
            start, 
            null,
            color, 
            angle, 
            Vector2.Zero,
            new Vector2(length, 1), 
            SpriteEffects.None,
            1f
        );
    }
}