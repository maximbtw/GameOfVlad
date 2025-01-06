using System;
using System.ComponentModel;
using GameOfVlad.Services.Graphic;
using GameOfVlad.Tools.Draw;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Entities;

public abstract class ColliderEntity(IServiceProvider serviceProvider) : Entity(serviceProvider)
{
    public virtual Color ColliderColor { get; set; } = Color.Red;

    public virtual float Rotation { get; set; } = 0f;

    // Точка вращения (по умолчанию — центр)
    public virtual Vector2 Origin => new(Size.Width / 2f, Size.Height / 2f);

    public virtual Rectangle Collider =>
        new((int)this.Position.X, (int)this.Position.Y, (int)this.Size.Width, (int)this.Size.Height);

    protected IGraphicService GraphicService => this.ServiceProvider.GetRequiredService<IGraphicService>();

    private ColliderDrawer _colliderDrawer;

    protected override void InitCore(ContentManager content)
    {
        _colliderDrawer = new ColliderDrawer(this, this.GraphicService);

        base.InitCore(content);
    }

    protected override void TerminateCore()
    {
        _colliderDrawer.Dispose();

        base.TerminateCore();
    }

    protected override void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Рисуем объект как повернутый прямоугольник
        spriteBatch.Draw(
            this.Texture,
            Position + Origin, // Позиция объекта с учетом центра вращения
            null,
            this.Color, // Цвет объекта (по умолчанию — белый)
            Rotation, // Угол поворота
            Origin, // Точка вращения (центр объекта)
            Vector2.One, // Масштабирование объекта до заданного размера
            SpriteEffects.None,
            0f
        );

        if (Settings.ShowCollider)
        {
            _colliderDrawer.DrawCollider(spriteBatch);
        }
    }
}