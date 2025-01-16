using System;
using GameOfVlad.Audio;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Entities.Player;
using GameOfVlad.GameObjects.Interfaces;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.Enemies.Forkfighter;

public partial class Forkfighter(ContentManager contentManager, IEffectDrawer effectDrawer)
    : HealthGameObject(contentManager), IHealth, IPlayerFinder
{
    public override float LayerDepth => (float)DrawOrderType.FrontEntity / 100f;
    public int UpdateOrder => 1;

    protected override Size ColliderSize => new(base.Size.Width * 0.5f, base.Size.Height * 0.75f);
    public override Vector2 Origin => new(this.Size.Width * 0.4f, this.Size.Height * 0.5f);

    public float Speed { get; set; } = 200f;
    public float AttackSpeed { get; set; } = 2f;

    private const float MinDistanceToMove = 50f;
    private const float MaxDistanceToMove = 2000f;

    private PlayerV2 _player;
    private bool _playerInAttackArea;
    private readonly Timer _attackTimer = new();

    protected override void LoadCore()
    {
        InitEffects();

        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        UpdateMovement(gameTime);
        UpdateAttack(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _movementAnimation.Update(gameTime);

        this.SpriteEffects = GameHelper.GetSpriteEffectByRotation(this.Rotation);

        base.Draw(gameTime, spriteBatch);
    }

    public void UpdateByPlayer(PlayerV2 player)
    {
        _player = player;
    }

    public void OnCollision(IColliderGameObject other)
    {

    }

    public void OnCollisionEnter(IColliderGameObject other)
    {
        if (other is PlayerV2)
        {
            _playerInAttackArea = true;
        }
    }

    public void OnCollisionExit(IColliderGameObject other)
    {
        if (other is PlayerV2)
        {
            _playerInAttackArea = false;
        }
    }

    private void UpdateMovement(GameTime gameTime)
    {
        if (_player == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(_player.CenterPosition, this.CenterPosition);
        Vector2 direction = GameHelper.CalculateDirection(this.CenterPosition, _player.CenterPosition);

        this.Rotation = MathF.Atan2(direction.Y, direction.X);

        bool needToMove = distanceToPlayer is <= MaxDistanceToMove and >= MinDistanceToMove;
        if (needToMove)
        {
            Vector2 velocity = direction * this.Speed;

            this.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    private void UpdateAttack(GameTime gameTime)
    {
        _attackTimer.Update(gameTime);

        bool canAttack = _playerInAttackArea && _attackTimer.Time >= AttackSpeed;
        if (canAttack)
        {
            this.AudioService.PlaySound(Sound.Enemy_Forkfighter_Hit);
            PlayAttackEffect();
            _player.TakeDamage(DamageStorage.Forkfighter);
            _attackTimer.Reset();
        }
    }
}