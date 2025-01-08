using System;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Utils;
using GameOfVlad.Utils.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameRenderer.Modificators;

public class LevelBorderRendererModificator(Vector2 startPosition, Size levelSize) : IRendererModificator
{
    private readonly LevelBoarderDrawer _levelBoarderDrawer = new();

    public void Update(IGameObject gameObject, GameTime gameTime)
    {
        if (gameObject is ILevelBorderRestrictedGameObject obj)
        {
            // Получаем вершины коллайдера
            Vector2[] colliderCorners = obj.GetCorners();

            // Вычисляем границы уровня
            float leftBorder = startPosition.X;
            float rightBorder = startPosition.X + levelSize.Width;
            float topBorder = startPosition.Y;
            float bottomBorder = startPosition.Y + levelSize.Height;

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
    }

    public void Draw(IGameObject gameObject, GameTime gameTime, SpriteBatch spriteBatch)
    {
        _levelBoarderDrawer.DrawLevelBorder(spriteBatch, startPosition, levelSize, Color.White);
    }
}