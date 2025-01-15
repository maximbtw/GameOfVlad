using GameOfVlad.GameObjects.Interfaces;

namespace GameOfVlad.Game.WeaponSystem;

public interface IProjectile : IColliderGameObject
{
    int Damage { get; set; }
}