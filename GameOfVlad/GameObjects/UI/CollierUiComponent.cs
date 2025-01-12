using System;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.Utils.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI;

public abstract class CollierUiComponent(ContentManager contentManager) : UiComponent(contentManager)
{
    public Color ColliderColor { get; set; } = Color.Red;
    
    private ColliderDrawer _colliderDrawer;
    
    public Vector2[] GetCorners()
    {
        Vector2 topLeft = this.PositionByCamera;
        Vector2 topRight = this.PositionByCamera + new Vector2(this.Size.Width, 0);
        Vector2 bottomLeft = this.PositionByCamera + new Vector2(0, this.Size.Height);
        Vector2 bottomRight = this.PositionByCamera + new Vector2(this.Size.Width, this.Size.Height);

        Vector2 center = this.PositionByCamera + this.Origin;

        return
        [
            RotatePoint(topLeft, center, this.Rotation),
            RotatePoint(topRight, center, this.Rotation),
            RotatePoint(bottomRight, center, this.Rotation),
            RotatePoint(bottomLeft, center, this.Rotation)
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
        // Для UI компонентов не обрабатываем
        return false;
    }

    public void OnCollision(IColliderGameObject other)
    {
        // Для UI компонентов не обрабатываем
    }
    
    public void OnCollisionEnter(IColliderGameObject other)
    {
        // Для UI компонентов не обрабатываем
    }

    public void OnCollisionExit(IColliderGameObject other)
    {
        // Для UI компонентов не обрабатываем
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