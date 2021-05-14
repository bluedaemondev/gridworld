using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// entidad base para player, enemigos y spawns
/// </summary>

public abstract class Entity : MonoBehaviour , IDamageable
{
    protected Health _health;
    protected Rigidbody _myRigidbody;

    public virtual void Start()
    {
        //_health = new Health(100); //===> pisar por cada tipo de entidad creando su vida desde la instancia
        _myRigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Die()
    {
        Debug.Log("Dying");
    }

    public bool IsDead()
    {
        return this._health.IsDead();
    }

    public virtual float TakeDamage(float value)
    {
        Debug.Log("Taking dmg " + value);
        if(0 >= this._health.TakeDamage(value))
        {
            this.Die();
        }

        return this._health.GetHealth();
    }

    

}
