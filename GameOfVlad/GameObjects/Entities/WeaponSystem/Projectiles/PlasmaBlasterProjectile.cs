using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Entities.WeaponSystem.Projectiles;

public class PlasmaBlasterProjectile(ContentManager contentManager) : ProjectileBase, IProjectile
{
    protected override void LoadCore()
    {
        this.Texture = contentManager.Load<Texture2D>(
            "2025/Sprites/Game/Weapons/Projectiles/PlasmaBlaster/plasma-bluster-projectile-39x24");
        
        base.LoadCore();
    }

    protected override void UpdateMovement(GameTime gameTime)
    {
        this.Position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.Rotation = MathF.Atan2(this.Velocity.Y, this.Velocity.X);
    }
    
    protected override void OnHit()
    {

    }
}