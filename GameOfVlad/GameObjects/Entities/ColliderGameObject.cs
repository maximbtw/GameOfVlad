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

    protected virtual Size ColliderSize => new(this.Texture?.Width ?? 0, this.Texture?.Height ?? 0);
    
    private ColliderDrawer _colliderDrawer;

    public virtual Vector2[] GetCorners()
    {
        float scaledWidth = this.ColliderSize.Width * this.Scale.X;
        float scaledHeight = this.ColliderSize.Height * this.Scale.Y;
        
        Vector2 topLeft = this.CenterPosition + new Vector2(-scaledWidth / 2, -scaledHeight / 2);
        Vector2 topRight = this.CenterPosition + new Vector2(scaledWidth / 2, -scaledHeight / 2);
        Vector2 bottomLeft = this.CenterPosition + new Vector2(-scaledWidth / 2, scaledHeight / 2);
        Vector2 bottomRight = this.CenterPosition + new Vector2(scaledWidth / 2, scaledHeight / 2);
        
        return
        [
            RotatePoint(topLeft, this.CenterPosition, Rotation),
            RotatePoint(topRight, this.CenterPosition, Rotation),
            RotatePoint(bottomRight, this.CenterPosition, Rotation),
            RotatePoint(bottomLeft, this.CenterPosition, Rotation)
        ];
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