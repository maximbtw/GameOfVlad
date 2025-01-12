using System;
using System.Collections.Generic;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.GameObjects.UI.Effects;
using GameOfVlad.GameRenderer;
using GameOfVlad.Services.Camera;
using GameOfVlad.Utils;
using GameOfVlad.Utils.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.GameObjects.Entities;

public class PlayerV2(ContentManager contentManager)
    : ColliderGameObject, ITrustForcePhysicalGameObject, ILevelBorderRestrictedGameObject, IHealth
{
    public int DrawOrder => (int)DrawOrderType.Player;
    public int UpdateOrder => 1;
    
    public int CurrentHP { get; private set; }

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
    public int MaxHP { get; set; } = 100;
    
    public event Action OnPlayerDeath;

    private readonly KeyboardInputObserver _keyboardInputObserver = new();
    private readonly ICameraService _cameraService = contentManager.ServiceProvider.GetRequiredService<ICameraService>();

    private ParticleGenerator _trustPowerParticleGenerator;
    private bool _trustPowerActive;
    private SpriteFont _font;

    protected override void LoadCore()
    {
        _trustPowerParticleGenerator = new ParticleGenerator(new ParticleGenerator.Configuration(contentManager)
        {
            CanProduceParticle = () => _trustPowerActive,
            GetSpawnPosition = () =>
            {
                Vector2[] corners = this.GetCorners();

                Vector2 bottomLeft = corners[0];
                Vector2 bottomRight = corners[3];

                return new Vector2((bottomLeft.X + bottomRight.X) / 2, (bottomLeft.Y + bottomRight.Y) / 2);
            },
            GetDirection = () => GameHelper.GetDirectionByRotationInRadians(this.Rotation) * -1,
            SpawnRate = 50,
            ParticleLifetime = 10,
            SpeedRange = Range<int>.Create(200, 500),
            OffsetAngleRange = Range<int>.Create(-45, 45),
            Colors = [
                Color.OrangeRed, 
                Color.OrangeRed, 
                Color.OrangeRed, 
                Color.White,     
                Color.DarkRed,
                Color.DarkRed,
                Color.DarkRed,
            ]
        })
        {
            Parent = this
        };
        
        this.CurrentHP = this.MaxHP;
        
        _font = contentManager.Load<SpriteFont>("Pages/MapLevels/Font");
        _keyboardInputObserver.KeyPressed += HandleKeyPressed;
        _cameraService.SetTargetPosition( () =>this.Position);

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Settings.Debug)
        {
            spriteBatch.DrawString(_font, this.Position.ToString(),
                new Vector2(this.Position.X - 900, this.Position.Y - 500), Color.Red);
        }

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

    public void OnCollision(IColliderGameObject other)
    {
    }

    public void OnCollisionEnter(IColliderGameObject other)
    {
    }

    public void OnCollisionExit(IColliderGameObject other)
    {
    }

    public void TakeDamage(int amount)
    {
        if (amount < 1)
        {
            throw new InvalidOperationException("Amount HP can't be less than 1");
        }
        
        CurrentHP -= amount;
        if (this.CurrentHP <= 0)
        {
            this.OnPlayerDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 1)
        {
            throw new InvalidOperationException("Amount HP can't be less than 1");
        }
        
        CurrentHP += amount;
    }
}