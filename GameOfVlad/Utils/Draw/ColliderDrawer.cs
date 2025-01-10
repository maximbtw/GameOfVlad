using System;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public class ColliderDrawer(IColliderGameObject gameObject) : IDisposable
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

        // Получаем углы коллайдера
        Vector2[] corners = gameObject.GetCorners();

        // Рисуем линии между углами
        DrawHelper.DrawLine(spriteBatch, corners[0], corners[1], gameObject.ColliderColor, _pixelTexture);
        DrawHelper.DrawLine(spriteBatch, corners[1], corners[2], gameObject.ColliderColor, _pixelTexture);
        DrawHelper.DrawLine(spriteBatch, corners[2], corners[3], gameObject.ColliderColor, _pixelTexture);
        DrawHelper.DrawLine(spriteBatch, corners[3], corners[0], gameObject.ColliderColor, _pixelTexture);
    }

    public void Dispose()
    {
        _pixelTexture?.Dispose();
    }
}