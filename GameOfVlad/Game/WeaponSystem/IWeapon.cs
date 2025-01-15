using GameOfVlad.GameObjects;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Game.WeaponSystem;

public interface IWeapon
{
    WeaponType Type { get; }
    
    float FireRate { get; set; }
    
    int Damage { get; set; }
    
    void Update(GameTime gameTime);

    void Shoot(IGameObject parent, Vector2 destinationPoint);
}