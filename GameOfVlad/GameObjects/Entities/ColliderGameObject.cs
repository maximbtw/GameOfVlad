using System;
using GameOfVlad.GameObjects.Interfaces;
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
        Vector2 topLeft = this.Position;
        Vector2 topRight = this.Position + new Vector2(this.Size.Width, 0);
        Vector2 bottomLeft = this.Position + new Vector2(0, this.Size.Height);
        Vector2 bottomRight = this.Position + new Vector2(this.Size.Width, this.Size.Height);

        Vector2 center = this.Position + this.Origin;

        return
        [
            RotatePoint(topLeft, center, Rotation),
            RotatePoint(topRight, center, Rotation),
            RotatePoint(bottomRight, center, Rotation),
            RotatePoint(bottomLeft, center, Rotation)
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
        if (Settings.ShowCollider)
        {
            _colliderDrawer?.DrawCollider(spriteBatch);
        }
        
        base.Draw(gameTime, spriteBatch);
    }

    private Vector2 RotatePoint(Vector2 point, Vector2 center, float angle)
    {
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);

        Vector2 translatedPoint = point - center;

        float rotatedX = translatedPoint.X * cos - translatedPoint.Y * sin;
        float rotatedY = translatedPoint.X * sin + translatedPoint.Y * cos;

        return new Vector2(rotatedX, rotatedY) + center;
    }
}