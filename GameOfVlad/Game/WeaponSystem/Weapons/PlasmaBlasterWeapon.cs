using GameOfVlad.Audio;
using GameOfVlad.Game.WeaponSystem.Projectiles;
using GameOfVlad.GameObjects;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameOfVlad.Game.WeaponSystem.Weapons;

public class PlasmaBlasterWeapon(ContentManager contentManager, IProjectileDrawer projectileDrawer)
    : WeaponBase<PlasmaBlasterProjectile>(contentManager, projectileDrawer), IWeapon
{
    public WeaponType Type => WeaponType.PlasmaBlaster;

    public override float FireRate { get; set; } = 1f;
    public int Damage { get; set; } = 10;

    protected override PlasmaBlasterProjectile CreateShot(IGameObject parent, Vector2 destinationPoint)
    {
        this.AudioService.PlaySound(Sound.Weapon_PlasmaBlaster_Shoot);
        
        Vector2 startPosition = parent.Position + parent.Origin;

        var projectile = new PlasmaBlasterProjectile(this.ContentManager)
        {
            Velocity = GameHelper.CalculateDirection(startPosition, destinationPoint) * 750,
            Damage = this.Damage,
            Position = startPosition,
            Parent = parent
        };

        return projectile;
    }
}