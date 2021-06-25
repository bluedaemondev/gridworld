using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Summoning Enemy
/// </summary>
public class EnemySummoner : Entity, IRagdoll
{
    public static EnemySummoner InstantiateEnemy(Vector3 position, Quaternion rot, Transform enemyContainerScene)
    {
        var result = Instantiate(LevelManager.instance.assets.enemySummoner_Prefab, position, rot, enemyContainerScene);
        return result.GetComponent<EnemySummoner>();
    }

    [SerializeField] private Animator _animator;

    public void Init()
    {
        this._health = new Health(25);
        LevelManager.instance.SubscribeAliveEntity(this);
    }

    public override void Start()
    {
        base.Start();
        Init();
    }

    public override float TakeDamage(float value)
    {
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingEnemySummoner);
        return base.TakeDamage(value);
    }

    public override void Die()
    {
        base.Die();
        this._animator.SetTrigger("die");
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingEnemySummoner);

        LevelManager.instance.RemoveEnemyFromAccountance(this);

        Destroy(this.gameObject, 3f);

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
