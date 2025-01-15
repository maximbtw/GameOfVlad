using GameOfVlad.GameObjects;

namespace GameOfVlad.Game.WeaponSystem;

public class ProjectileDrawer : DrawerGameObject, IProjectileDrawer
{
    public int DrawOrder => (int)DrawOrderType.Projectile;
    public int UpdateOrder => 0;
    
    public void AddProjectile(IProjectile projectile)
    {
        AddGameObject(projectile);
    }

    public void ClearProjectiles()
    {
        Clear();
    }
}