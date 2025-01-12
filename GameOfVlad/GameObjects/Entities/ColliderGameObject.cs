using System;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.Utils;
using GameOfVlad.Utils.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities;

public abstract class ColliderGameObject : GameObject
{
    public Color ColliderColor { get; set; } = Color.Red;

    private ColliderDrawer _colliderDrawer;

    public virtual Vector2[] GetCorners()
    {
        // Учитываем масштабированные размеры
        float scaledWidth = this.Size.Width * this.Scale.X;
        float scaledHeight = this.Size.Height * this.Scale.Y;

        // Перерассчитываем позицию с учетом масштаба
        Vector2 adjustedPosition = this.Position - (this.Origin * this.Scale - this.Origin);

        // Углы объекта до вращения
        Vector2 topLeft = adjustedPosition;
        Vector2 topRight = adjustedPosition + new Vector2(scaledWidth, 0);
        Vector2 bottomLeft = adjustedPosition + new Vector2(0, scaledHeight);
        Vector2 bottomRight = adjustedPosition + new Vector2(scaledWidth, scaledHeight);

        // Центр вращения с учетом новой позиции
        Vector2 center = adjustedPosition + (this.Origin * this.Scale);

        // Возвращаем углы с учетом вращения
        return new Vector2[]
        {
            RotatePoint(topLeft, center, Rotation),
            RotatePoint(topRight, center, Rotation),
            RotatePoint(bottomRight, center, Rotation),
            RotatePoint(bottomLeft, center, Rotation)
        };
    }

    protected override void LoadCore()
    {
        _colliderDrawer = new ColliderDrawer((IColliderGameObject)this);

        base.LoadCore();
    }

    protected override void UnloadCore()
    {
        _colliderDrawer.Dispose();

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        if (Settings.Debug)
        {
            _colliderDrawer?.DrawCollider(spriteBatch);
        }
    }

    public bool Intersects(IColliderGameObject other)
    {
        return CollisionHelper.CheckCollision(this.GetCorners(), other.GetCorners());
    }

    protected Vector2 RotatePoint(Vector2 point, Vector2 center, float angle)
    {
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);

        Vector2 translatedPoint = point - center;

        float rotatedX = translatedPoint.X * cos - translatedPoint.Y * sin;
        float rotatedY = translatedPoint.X * sin + translatedPoint.Y * cos;

        return new Vector2(rotatedX, rotatedY) + center;
    }
}