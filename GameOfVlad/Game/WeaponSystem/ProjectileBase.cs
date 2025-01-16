using GameOfVlad.GameObjects;
using GameOfVlad.GameObjects.Effects;
using GameOfVlad.GameObjects.Entities;
using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game.WeaponSystem;

public abstract class ProjectileBase(ContentManager contentManager, IEffectDrawer effectDrawer) : ColliderGameObject(contentManager)
{
    public override float LayerDepth => (float)DrawOrderType.Projectile / 100f;
    public int UpdateOrder => 1;
    
    public int Damage { get; set; }

    protected readonly IEffectDrawer EffectDrawer = effectDrawer;

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