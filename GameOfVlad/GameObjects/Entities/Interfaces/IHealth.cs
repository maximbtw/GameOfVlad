namespace GameOfVlad.GameObjects.Entities.Interfaces;

public interface IHealth
{
    int CurrentHP { get; }
    
    int MaxHP { get; set; }

    void TakeDamage(int amount);
    
    void Heal(int amount);
}