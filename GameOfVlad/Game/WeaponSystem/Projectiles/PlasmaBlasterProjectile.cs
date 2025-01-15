using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Game.WeaponSystem.Projectiles;

public class PlasmaBlasterProjectile(ContentManager contentManager) : ProjectileBase, IProjectile
{
    public Vector2 Velocity { get; init; }
    
    private const float Distance = 1500;
    
    private Vector2 _startPosition;
    
    protected override void LoadCore()
    {
        _startPosition = this.Position;
        
        this.Texture = contentManager.Load<Texture2D>(
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

    }
}