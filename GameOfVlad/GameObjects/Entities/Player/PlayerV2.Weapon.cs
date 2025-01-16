using GameOfVlad.Game.WeaponSystem.Weapons;
using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.Player;

public partial class PlayerV2
{
    private readonly WeaponManager _weaponManager = new();

    private void InitWeaponManager()
    {
        _weaponManager.AddWeapon(new PlasmaBlasterWeapon(ContentManager, effectDrawer, projectileDrawer));
        _weaponManager.AddWeapon(new DartcasterWeapon(ContentManager, effectDrawer, projectileDrawer));

        _mouseInput.OnScrollWheelUp += (_, _) => _weaponManager.NextWeapon();
        _mouseInput.OnScrollWheelDown += (_, _) => _weaponManager.PrevWeapon();
    }

    private void UpdateWeapons(GameTime gameTime)
    {
        _weaponManager.Update(gameTime);

        if (_mouseInput.IsLeftButtonPressed())
        {
            Vector2 mousePosition = this.CameraService.PositionByCamera(_mouseInput.GetMousePosition());

            _weaponManager.Shoot(this, mousePosition);
        }
    }
}