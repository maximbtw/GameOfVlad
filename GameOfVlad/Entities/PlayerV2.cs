using System;
using GameOfVlad.Entities.Interfaces;
using GameOfVlad.Services.Camera;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Entities;

public class PlayerV2(IServiceProvider serviceProvider)
    : ColliderEntity(serviceProvider), ITrustForcePhysicalGameObject, ILevelBorderRestrictedGameObject
{
    public override int DrawOrder => (int)DrawOrderType.Player;
    public override int UpdateOrder => 1;

    public float Mass { get; set; } = 1f;
    public float MaxVelocity { get; set; } = 300f;
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public float TrustPower { get; set; } = 50f;

    private readonly KeyboardStateObserver _keyboardStateObserver =
        serviceProvider.GetRequiredService<KeyboardStateObserver>();

    private readonly ICameraService _cameraService =
        serviceProvider.GetRequiredService<ICameraService>();

    protected override void InitCore(ContentManager content)
    {
        _keyboardStateObserver.OnUpdated += HandleUpdateKeyboard;

        base.InitCore(content);
    }

    protected override void UpdateCore(GameTime gameTime)
    {
        _keyboardStateObserver.Update(gameTime);

        base.UpdateCore(gameTime);
    }

    protected override void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _cameraService.SetCameraPosition(this.Position, spriteBatch.GraphicsDevice);

        base.DrawCore(gameTime, spriteBatch);
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

    public void OnLevelBorderCollision(Vector2 collisionNormal)
    {
        // Отражаем скорость относительно нормали столкновения
        this.Velocity = Vector2.Reflect(this.Velocity, collisionNormal) * 0.5f; // Замедляем после отражения

        // Альтернатива: полностью обнуляем компонент скорости в направлении нормали
        //this.Velocity -= Vector2.Dot(this.Velocity, collisionNormal) * collisionNormal;
    }
}