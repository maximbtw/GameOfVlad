using GameOfVlad.GameRenderer;

namespace GameOfVlad.GameObjects.Entities.WeaponSystem;

public interface IProjectileDrawer : IRendererObject
{
    public void AddProjectile(IProjectile projectile);
    
    public void ClearProjectiles();
}