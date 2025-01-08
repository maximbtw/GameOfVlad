using System;
using GameOfVlad.Utils.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Entities;

public abstract class ColliderEntity(IServiceProvider serviceProvider) : Entity(serviceProvider)
{
    public virtual Color ColliderColor { get; set; } = Color.Red;
    public virtual float Rotation { get; set; } = 0f;
    public virtual float RotationVelocity { get; set; } = 1f;
    
    public virtual Vector2 Origin => new(Size.Width / 2f, Size.Height / 2f);

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

    public virtual Vector2 RotatePoint(Vector2 point, Vector2 center, float angle)
    {
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);

        Vector2 translatedPoint = point - center;

        float rotatedX = translatedPoint.X * cos - translatedPoint.Y * sin;
        float rotatedY = translatedPoint.X * sin + translatedPoint.Y * cos;

        return new Vector2(rotatedX, rotatedY) + center;
    }

    protected override void InitCore(ContentManager content)
    {
        _colliderDrawer = new ColliderDrawer(this);

        base.InitCore(content);
    }

    protected override void TerminateCore()
    {
        _colliderDrawer.Dispose();

        base.TerminateCore();
    }

    protected override void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            this.Texture,
            this.Position + this.Origin, // Позиция объекта с учетом центра вращения
            null,
            this.Color, // Цвет объекта (по умолчанию — белый)
            this.Rotation,
            this.Origin, // Точка вращения (центр объекта)
            Vector2.One, // Масштабирование объекта до заданного размера
            SpriteEffects.None,
            0f
        );
        
        if (Settings.ShowCollider)
        {
            _colliderDrawer.DrawCollider(spriteBatch, this);
        }
    }
}