using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public class RectangleDrawer : IDisposable
{
    // Статическая текстура 1x1 пиксель (общая для всех сущностей)
    private Texture2D _pixelTexture;

    public void DrawCollider(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
    {
        if (_pixelTexture == null)
        {
            _pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _pixelTexture.SetData([Color.White]);
        }

        // Верхняя линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Left, rectangle.Top), 
            new Vector2(rectangle.Right, rectangle.Top), 
            color, 
            _pixelTexture);

        // Правая линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Right, rectangle.Top), 
            new Vector2(rectangle.Right, rectangle.Bottom), 
            color, 
            _pixelTexture);

        // Нижняя линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Right, rectangle.Bottom), 
            new Vector2(rectangle.Left, rectangle.Bottom), 
            color, 
            _pixelTexture);

        // Левая линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Left, rectangle.Bottom), 
            new Vector2(rectangle.Left, rectangle.Top), 
            color, 
            _pixelTexture);
    }

    public void Dispose()
    {
        _pixelTexture?.Dispose();
    }
}