using GameOfVlad.GameObjects.Interfaces;

namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface IHealth : IColliderGameObject
{
    int CurrentHP { get; }
    
    int MaxHP { get; set; }

    void TakeDamage(int amount);
    
    void Heal(int amount);
}