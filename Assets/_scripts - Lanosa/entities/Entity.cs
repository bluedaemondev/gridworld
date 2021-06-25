using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// entidad base para player, enemigos y spawns
/// </summary>

public abstract class Entity : MonoBehaviour, IDamageable
{
    protected Health _health;
    protected Rigidbody _myRigidbody;
    protected GameObject killParticles;

    private void Awake()
    {
        _health = new Health(1);

    }
    public virtual void Start()
    {
        //_health = new Health(100); //===> pisar por cada tipo de entidad creando su vida desde la instancia
        _myRigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    public virtual void Die()
    {
        this._health.Die();
        Debug.Log("Dying");
    }

    public bool IsDead()
    {
        return this._health.IsDead();
    }

    public virtual float TakeDamage(float value)
    {
        if (IsDead())
            return 0;

        Debug.Log("Taking dmg " + value);
        EffectFactory.instance.ShakeCamera(0.3f, 2);

        if (0 >= this._health.TakeDamage(value))
        {
            this.Die();
        }

        return this._health.GetHealth();
    }



}
