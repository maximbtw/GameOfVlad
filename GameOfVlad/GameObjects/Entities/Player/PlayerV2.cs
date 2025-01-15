using System;
using System.Collections.Generic;
using GameOfVlad.Game.WeaponSystem;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Camera;
using GameOfVlad.Services.Game;
using GameOfVlad.Utils.Keyboards;
using GameOfVlad.Utils.Mouse;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = GameOfVlad.Utils.Keyboards.KeyboardInput;

namespace GameOfVlad.GameObjects.Entities.Player;

public partial class PlayerV2(ContentManager contentManager, IEffectDrawer effectDrawer, IProjectileDrawer projectileDrawer)
    : HealthGameObject, ITrustForcePhysicalGameObject, ILevelBorderRestrictedGameObject, IHealth
{
    public int DrawOrder => (int)DrawOrderType.Player;
    public int UpdateOrder => 1;
    

    public override IEnumerable<IRendererObject> ChildrenBefore 
    {
        get
        {
            yield return _trustPowerParticleGenerator;
        }
        set => throw new NotSupportedException();
    }

    public float Mass { get; set; } = 1f;
    public float MaxVelocity { get; set; } = 300f;
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public float TrustPower { get; set; } = 50f;
    public float RotationVelocity { get; set; } = 3f;
    
    public event Action OnPlayerDeath;

    private readonly KeyboardInput _keyboardInput = new();
    private readonly MouseInput _mouseInput = new();
    
    private ICameraService CameraService => contentManager.ServiceProvider.GetRequiredService<ICameraService>();
    private IGameService GameService => contentManager.ServiceProvider.GetRequiredService<IGameService>();
    
    private bool _trustPowerActive;
    private SpriteFont _font;

    protected override void LoadCore()
    {
        InitParticleEffects();
        InitWeaponManager();
        
        _font = contentManager.Load<SpriteFont>("Pages/MapLevels/Font");
        _keyboardInput.KeyPressed += HandleKeyPressed;
        CameraService.SetTargetPosition( () =>this.Position);

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Settings.Debug)
        {
            spriteBatch.DrawString(
                _font, 
                this.Position.ToString(),
                new Vector2(this.Position.X - 1200, this.Position.Y - 700), 
                Color.Green);
            
            spriteBatch.DrawString(
                _font, 
                _weaponManager.GetCurrentWeaponType().ToString(),
                new Vector2(this.Position.X - 1200, this.Position.Y - 650),
                Color.Green);
            
            spriteBatch.DrawString(
                _font, 
                $"{this.GameService.GetCurrentFps()} FPS",
                new Vector2(this.Position.X - 1200, this.Position.Y - 600), 
                Color.Green);
        }

        base.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        _trustPowerActive = false;
        _keyboardInput.Update();
        _mouseInput.Update();
        UpdateWeapons(gameTime);
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

    public void OnCollision(IColliderGameObject other)
    {
    }

    public void OnCollisionEnter(IColliderGameObject other)
    {
    }

    public void OnCollisionExit(IColliderGameObject other)
    {
    }

    protected override void OnZeroHP()
    {
        this.OnPlayerDeath?.Invoke();
    }
}