using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.Services.Camera;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.GameObjects.Entities;

public class PlayerV2(ContentManager contentManager)
    : ColliderGameObject, ITrustForcePhysicalGameObject, ILevelBorderRestrictedGameObject
{
    public int DrawOrder => (int)DrawOrderType.Player;
    public int UpdateOrder => 1;

    public float Mass { get; set; } = 1f;
    public float MaxVelocity { get; set; } = 300f;
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public float TrustPower { get; set; } = 50f;
    public float RotationVelocity { get; set; } = 5f;
    

    private readonly KeyboardInputObserver _keyboardInputObserver = new();

    private readonly ICameraService _cameraService =
        contentManager.ServiceProvider.GetRequiredService<ICameraService>();

    private bool _trustPowerActive;

    protected override void LoadCore()
    {
        _keyboardInputObserver.KeyPressed += HandleKeyPressed;

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _cameraService.SetCameraPosition(this.Position);

        base.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        _trustPowerActive = false;
        _keyboardInputObserver.Update();
    }

    public void OnLevelBorderCollision(Vector2 collisionNormal)
    {
        // Отражаем скорость относительно нормали столкновения
        this.Velocity = Vector2.Reflect(this.Velocity, collisionNormal) * 0.5f; // Замедляем после отражения

        // Альтернатива: полностью обнуляем компонент скорости в направлении нормали
        //this.Velocity -= Vector2.Dot(this.Velocity, collisionNormal) * collisionNormal;
    }

    private void HandleKeyPressed(KeyEventArgs e)
    {
        float rotationSpeed = MathHelper.ToRadians(this.RotationVelocity);
        if (e.Key == Keys.A)
        {
            Rotate(-rotationSpeed);
        }

        if (e.Key == Keys.D)
        {
            Rotate(rotationSpeed);
        }

        if (e.Key == Keys.Space)
        {
            _trustPowerActive = true;
        }

        void Rotate(float rotationChange)
        {
            this.Rotation += rotationChange;

            this.Rotation = MathHelper.WrapAngle(this.Rotation);
        }
    }

    public float GetTrustForce()
    {
        return _trustPowerActive ? this.TrustPower : 0f;
    }
}