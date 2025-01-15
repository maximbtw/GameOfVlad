using System;
using GameOfVlad.GameObjects.Entities;
using GameOfVlad.GameObjects.Entities.Interfaces;

namespace GameOfVlad.GameObjects;

public abstract class HealthGameObject : ColliderGameObject
{
    public int CurrentHP { get; private set; }

    public int MaxHP { get; set; } = 100;

    protected override void LoadCore()
    {
        this.CurrentHP = this.MaxHP;
        
        base.LoadCore();
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount < 1)
        {
            throw new InvalidOperationException("Amount HP can't be less than 1");
        }
        
        this.CurrentHP -= amount;
        if (this.CurrentHP <= 0)
        {
            OnZeroHP();
        }
    }

    public virtual void Heal(int amount)
    {
        if (amount < 1)
        {
            throw new InvalidOperationException("Amount HP can't be less than 1");
        }
        
        this.CurrentHP += amount;
        if (this.CurrentHP > this.MaxHP)
        {
            this.CurrentHP = this.MaxHP;
        }
    }
    
    protected virtual void OnZeroHP()
    {
        Destroy();
    }
}