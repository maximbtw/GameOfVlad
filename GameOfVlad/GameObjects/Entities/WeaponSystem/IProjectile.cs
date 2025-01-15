using GameOfVlad.GameObjects.Interfaces;

namespace GameOfVlad.GameObjects.Entities.WeaponSystem;

public interface IProjectile : IColliderGameObject
{
    int Damage { get; set; }
}