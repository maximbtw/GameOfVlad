using GameOfVlad.GameObjects.Entities.Interfaces;
using GameOfVlad.GameObjects.Interfaces;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.WeaponSystem;

public abstract class ProjectileBase : ColliderGameObject
{
    public int DrawOrder => (int)DrawOrderType.Projectile;
    
    public int UpdateOrder => 1;
    
    public int Damage { get; set; }
    public Vector2 Velocity { get; init; }
    public float Distance { get; init; }

    private Vector2 _startPosition;

    protected override void LoadCore()
    {
        _startPosition = this.Position;
        
        base.LoadCore();
    }

    public override void Update(GameTime gameTime)
    {
        UpdateMovement(gameTime);
        
        float distance = Vector2.Distance(_startPosition, this.Position);
        if (distance >= this.Distance)
        {
            Destroy();
        }
        
        base.Update(gameTime);
    }

    public void OnCollision(IColliderGameObject other)
    {
    }

    public void OnCollisionEnter(IColliderGameObject other)
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

    public void OnCollisionExit(IColliderGameObject other)
    {
    }

    protected abstract void UpdateMovement(GameTime gameTime);
    
    protected abstract void OnHit();
}