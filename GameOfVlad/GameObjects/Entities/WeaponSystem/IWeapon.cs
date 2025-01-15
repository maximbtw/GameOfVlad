using Microsoft.Xna.Framework;

namespace GameOfVlad.GameObjects.Entities.WeaponSystem;

public interface IWeapon
{
    WeaponType Type { get; }
    
    float FireRate { get; set; }
    
    void Update(GameTime gameTime);

    void Shoot(IGameObject parent, Vector2 destinationPoint);
}