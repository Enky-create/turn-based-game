using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public event EventHandler OnDeath;
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public int health;
    }
    public void TakeDamage(int damage)
    {
        health-=damage;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
        {
            health = this.health
        });
        if (health <= 0)
        {
            health = 0;
            OnDeath?.Invoke(this,EventArgs.Empty);
        }
    }
    public void AddHealth(int healthAmount)
    {
        health+=healthAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
        {
            health = this.health
        });

    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
