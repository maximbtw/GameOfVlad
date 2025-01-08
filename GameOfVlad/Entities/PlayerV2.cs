using System;
using GameOfVlad.Game.Physics;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Entities;

public class PlayerV2(IServiceProvider serviceProvider)
    : ColliderEntity(serviceProvider), ITrustForcedGameObject
{
    public override int DrawOrder => (int)DrawOrderType.Player;
    public override int UpdateOrder => 1;

    public float RotationVelocity { get; set; } = 1f;
    public float Mass { get; set;} = 1f;
    public float MaxVelocity { get; set; } = 300f;
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public float TrustPower { get; set; } = 50f;


    private readonly KeyboardStateObserver _keyboardStateObserver =
        serviceProvider.GetRequiredService<KeyboardStateObserver>();

    protected override void InitCore(ContentManager content)
    {
        _keyboardStateObserver.OnUpdated += HandleUpdateKeyboard;

        base.InitCore(content);
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

        base.DrawCore(gameTime, spriteBatch);
    }

    protected override void UpdateCore(GameTime gameTime)
    {
        _keyboardStateObserver.Update(gameTime);

        base.UpdateCore(gameTime);
    }

    private void HandleUpdateKeyboard(object sender, KeyboardStateEventArgs e)
    {
        float rotationSpeed = MathHelper.ToRadians(this.RotationVelocity); 
        if (e.Key.IsKeyDown(Keys.A))
        {
            Rotate(-rotationSpeed);
        }
        else if (e.Key.IsKeyDown(Keys.D))
        {
            Rotate(rotationSpeed);
        }

        void Rotate(float rotationChange)
        {
            this.Rotation += rotationChange;

            this.Rotation = MathHelper.WrapAngle(this.Rotation);
        }
    }
}