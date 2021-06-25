using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : IDamageable
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private float healthMax = 100;
    private float health;

    //default -> flag use max
    public Health(float healthMax, float currentHealth = -1)
    {
        this.healthMax = healthMax;
        health = currentHealth > -1 ? currentHealth : healthMax;
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetHealthNormalized()
    {
        return health / healthMax;
    }

    public float TakeDamage(float amount)
    {
        health -= amount;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        
        if (IsDead())
        {
            Die();
        }

        return this.health;
    }

    public void Die()
    {
        health = 0;
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > healthMax)
        {
            health = healthMax;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
}
