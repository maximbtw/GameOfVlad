using GameOfVlad.Game.WeaponSystem.Projectiles;
using GameOfVlad.GameObjects;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game.WeaponSystem.Weapons;

public class DartcasterWeapon(ContentManager contentManager, IProjectileDrawer projectileDrawer)
    : WeaponBase<DartcasterProjectile>(contentManager, projectileDrawer), IWeapon
{
    public WeaponType Type => WeaponType.Dartcaster;

    public override float FireRate { get; set; } = 1f;
    public int Damage { get; set; } = 10;
    

    protected override DartcasterProjectile CreateShot(IGameObject parent, Vector2 destinationPoint)
    {
        Vector2 startPosition = parent.Position + parent.Origin;

        var projectile = new DartcasterProjectile(this.ContentManager)
        {
            Velocity = GameHelper.CalculateDirection(startPosition, destinationPoint) * 1200,
            Damage = this.Damage,
            Position = startPosition,
            Parent = parent
        };

        return projectile;
    }
}