using GameOfVlad.GameObjects.Entities.WeaponSystem.Projectiles;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.GameObjects.Entities.WeaponSystem.Weapons;

public class PlasmaBlasterWeapon(ContentManager contentManager, IProjectileDrawer projectileDrawer)
    : WeaponBase<PlasmaBlasterProjectile>(contentManager, projectileDrawer), IWeapon
{
    public WeaponType Type => WeaponType.PlasmaBlaster;

    public override float FireRate { get; set; } = 1f;

    protected override PlasmaBlasterProjectile CreateShot(IGameObject parent, Vector2 destinationPoint)
    {
        Vector2 startPosition = parent.Position + parent.Origin;

        var projectile = new PlasmaBlasterProjectile(this.ContentManager)
        {
            Velocity = GameHelper.CalculateDirection(startPosition, destinationPoint) * 750,
            Damage = 10,
            Distance = 1500,
            Position = startPosition,
            Parent = parent
        };

        return projectile;
    }
}