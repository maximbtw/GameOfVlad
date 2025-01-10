using System;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Utils;
using GameOfVlad.Utils.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer.GameObjectRendererModificators;

public class LevelBorderRendererModificator(Rectangle levelBounds)
    : BaseRendererObjectHandler<ILevelBorderRestrictedGameObject>, IRendererObjectHandler
{
    private readonly RectangleDrawer _rectangleDrawer = new();

    protected override void UpdateCore(ILevelBorderRestrictedGameObject obj, GameTime gameTime)
    {
        // Получаем вершины коллайдера
        Vector2[] colliderCorners = obj.GetCorners();

        // Вычисляем границы уровня
        float leftBorder = levelBounds.X;
        float rightBorder = levelBounds.X + levelBounds.Width;
        float topBorder = levelBounds.Y;
        float bottomBorder = levelBounds.Y + levelBounds.Height;

        // Смещение, необходимое для возврата в границы
        Vector2 offset = Vector2.Zero;
        Vector2 collisionNormal = Vector2.Zero; // Направление столкновения

        foreach (var corner in colliderCorners)
        {
            if (corner.X < leftBorder)
            {
                offset.X = MathF.Max(offset.X, leftBorder - corner.X);
                collisionNormal = new Vector2(1, 0); // Отталкивание вправо
            }

            if (corner.X > rightBorder)
            {
                offset.X = MathF.Min(offset.X, rightBorder - corner.X);
                collisionNormal = new Vector2(-1, 0); // Отталкивание влево
            }

            if (corner.Y < topBorder)
            {
                offset.Y = MathF.Max(offset.Y, topBorder - corner.Y);
                collisionNormal = new Vector2(0, 1); // Отталкивание вниз
            }

            if (corner.Y > bottomBorder)
            {
                offset.Y = MathF.Min(offset.Y, bottomBorder - corner.Y);
                collisionNormal = new Vector2(0, -1); // Отталкивание вверх
            }
        }

        // Если есть смещение, применяем его и вызываем метод при столкновении
        if (offset != Vector2.Zero)
        {
            obj.Position += offset;
            obj.OnLevelBorderCollision(collisionNormal);
        }
    }

    protected override void DrawCore(ILevelBorderRestrictedGameObject obj, GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Settings.ShowCollider)
        {
            _rectangleDrawer.DrawCollider(spriteBatch, levelBounds, Color.Red);
        }
    }
}