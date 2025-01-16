using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Entities;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Interfaces;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.WeaponSystem;

public abstract class ProjectileBase : ColliderGameObject
{
    public override float LayerDepth => (float)DrawOrderType.Projectile / 100f;
    public int UpdateOrder => 1;
    
    public int Damage { get; set; }

    public virtual void OnCollision(IColliderGameObject other)
    {
    }

    public virtual void OnCollisionEnter(IColliderGameObject other)
    {
        if (other == this.Parent)
        {
            return;
        }
        
        if (other is IHealth health)
        {
            health.TakeDamage(this.Damage);

            OnHit();
            
            Destroy();
        }
    }

    public virtual void OnCollisionExit(IColliderGameObject other)
    {
    }
    
    protected abstract void OnHit();
}