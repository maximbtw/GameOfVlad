using System;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Utils;
using GameOfVlad.Utils.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities;

public abstract class ColliderEntity(ContentManager contentManager) : Entity(contentManager)
{
    public Color ColliderColor { get; set; } = Color.Red;
    public float Rotation { get; set; }
    public float RotationVelocity { get; set; } = 1f;
    
    public virtual Size Size => Size.Create(this.Texture.Width, this.Texture.Height);
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

    public override void Init()
    {
        _colliderDrawer = new ColliderDrawer((IColliderGameObject)this);

        base.Init();
    }

    public override void Terminate()
    {
        _colliderDrawer.Dispose();

        base.Terminate();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            this.Texture,
            this.Position + this.Origin, 
            null,
            this.Color, 
            this.Rotation,
            this.Origin, 
            Vector2.One, 
            SpriteEffects.None,
            0f
        );
        
        if (Settings.ShowCollider)
        {
            _colliderDrawer.DrawCollider(spriteBatch);
        }
    }
}