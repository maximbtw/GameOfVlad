using System;
using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.WeaponSystem.Projectiles;

public class PlasmaBlasterProjectile(ContentManager contentManager, IEffectDrawer effectDrawer)
    : ProjectileBase(contentManager, effectDrawer), IProjectile
{
    public Vector2 Velocity { get; init; }

    private const float Distance = 1500;

    private Vector2 _startPosition;

    protected override void LoadCore()
    {
        _startPosition = this.Position;

        this.Texture = ContentManager.Load<Texture2D>(
            "2025/Sprites/Game/Weapons/Projectiles/PlasmaBlaster/plasma-bluster-projectile-39x24");

        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        UpdateMovement(gameTime);

        float distance = Vector2.Distance(_startPosition, this.Position);
        if (distance >= Distance)
        {
            Destroy();
        }

        base.Update(gameTime);
    }

    private void UpdateMovement(GameTime gameTime)
    {
        this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.Rotation = MathF.Atan2(this.Velocity.Y, this.Velocity.X);
    }

    protected override void OnHit()
    {
        AnimationHelper.AddSplashEffect(
            this.ContentManager,
            this.EffectDrawer,
            textureNumber: "002",
            countTexture: 12,
            this.CenterPosition,
            Color.Red,
            scale: Vector2.One,
            layerDepth: (float)DrawOrderType.FrontEffect / 100f,
            timePerFrame: 0.02f
        );
    }
}