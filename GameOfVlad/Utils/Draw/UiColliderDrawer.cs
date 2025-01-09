using System;
using GameOfVlad.GameObjects.UI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public class UiColliderDrawer(IUiComponent component) : IDisposable
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

        var rectangle = component.BoundingBox;

        // Верхняя линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Left, rectangle.Top), 
            new Vector2(rectangle.Right, rectangle.Top), 
            Color.Red, 
            _pixelTexture);

        // Правая линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Right, rectangle.Top), 
            new Vector2(rectangle.Right, rectangle.Bottom), 
            Color.Red, 
            _pixelTexture);

        // Нижняя линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Right, rectangle.Bottom), 
            new Vector2(rectangle.Left, rectangle.Bottom), 
            Color.Red, 
            _pixelTexture);

        // Левая линия
        DrawHelper.DrawLine(spriteBatch, 
            new Vector2(rectangle.Left, rectangle.Bottom), 
            new Vector2(rectangle.Left, rectangle.Top), 
            Color.Red, 
            _pixelTexture);
    }

    public void Dispose()
    {
        _pixelTexture?.Dispose();
    }
}