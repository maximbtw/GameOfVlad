using System;
using GameOfVlad.GameObjects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public class ColliderDrawer(ColliderEntity entity) : IDisposable
{
    // Статическая текстура 1x1 пиксель (общая для всех сущностей)
    private Texture2D _pixelTexture;

    public void DrawCollider(SpriteBatch spriteBatch, ColliderEntity entity)
    {
        if (_pixelTexture == null)
        {
            _pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _pixelTexture.SetData([Color.White]);
        }

        // Получаем углы коллайдера
        Vector2[] corners = entity.GetCorners();

        // Рисуем линии между углами
        DrawHelper.DrawLine(spriteBatch, corners[0], corners[1], entity.ColliderColor, _pixelTexture);
        DrawHelper.DrawLine(spriteBatch, corners[1], corners[2], entity.ColliderColor, _pixelTexture);
        DrawHelper.DrawLine(spriteBatch, corners[2], corners[3], entity.ColliderColor, _pixelTexture);
        DrawHelper.DrawLine(spriteBatch, corners[3], corners[0], entity.ColliderColor, _pixelTexture);
    }

    public void Dispose()
    {
        _pixelTexture?.Dispose();
    }
}