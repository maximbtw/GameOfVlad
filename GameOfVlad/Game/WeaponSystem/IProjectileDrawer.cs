using GameOfVlad.GameRenderer;

namespace GameOfVlad.Game.WeaponSystem;

public interface IProjectileDrawer : IRendererObject
{
    public void AddProjectile(IProjectile projectile);
    
    public void ClearProjectiles();
}