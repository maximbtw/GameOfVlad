using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.WeaponSystem;

public interface IWeapon
{
    WeaponType Type { get; }
    
    int Damage { get; set; }
    
    float FireRate { get; set; }

    void Shoot(Vector2 direction);
}