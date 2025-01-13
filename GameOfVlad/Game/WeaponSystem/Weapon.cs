using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.WeaponSystem;

public class Weapon : IWeapon
{
    public WeaponType Type { get; }
    public int Damage { get; set; }
    public float FireRate { get; set; }
    
    public void Shoot(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }
}