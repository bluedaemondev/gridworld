using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shooting Enemy
/// </summary>
public class EnemyShooter : Entity, IRagdoll
{
    public static EnemyShooter InstantiateEnemy(Vector3 position, Quaternion rot, Transform enemyContainerScene)
    {
        var result = Instantiate(LevelManager.instance.assets.enemyShooter_Prefab, position, rot, enemyContainerScene);
        return result.GetComponent<EnemyShooter>();
    }

    [SerializeField] private Animator _animator;
    
    public void Init()
    {
        this._health = new Health(50);
        LevelManager.instance.SubscribeAliveEntity(this);
    }

    public override void Start()
    {
        base.Start();
        Init();
    }

    public override float TakeDamage(float value)
    {
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingEnemyShooter);
        return base.TakeDamage(value);
    }

    public override void Die()
    {
        base.Die();
        this._animator.SetTrigger("die");
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingEnemyShooter);

        LevelManager.instance.RemoveEnemyFromAccountance(this);

    }

    public void EnableRagdollPhysics()
    {
        //_myRigidbody.isKinematic = false;
        Component[] components = GetComponentsInChildren(typeof(Transform));

        foreach (Component c in components)
        {
            if (c.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
        }
    }

    public void DisableRagdollPhysics()
    {
        //_myRigidbody.isKinematic = true;
        Component[] components = GetComponentsInChildren(typeof(Transform));

        foreach (Component c in components)
        {
            if (c.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
        }
    }
}
