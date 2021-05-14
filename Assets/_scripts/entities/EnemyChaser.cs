using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chasing enemy
/// </summary>
public class EnemyChaser : Entity, IAttacker, IMovable, IRagdoll
{
    public static EnemyChaser InstantiateEnemy(Vector3 position, Quaternion rot, Transform enemyContainerScene)
    {
        var result = Instantiate(LevelManager.instance.assets.enemyChaser_Prefab, position, rot, enemyContainerScene);
        return result.GetComponent<EnemyChaser>();
    }

    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider attackArea;

    [SerializeField] private TargetChaser _chaser;

    [SerializeField] private string walkingBoolName = "isWalking";


    public void Init()
    {
        this._health = new Health(50);

        if (attackArea != null)
            this.attackArea.enabled = false;

        if (_chaser == null)
            _chaser = GetComponent<TargetChaser>();

        if (_animator == null)
            _animator = GetComponent<Animator>();
        

        DisableRagdollPhysics();

        _chaser.Init(this);
        _chaser.StartChasingTarget(LevelManager.instance.player);
    }

    public override void Start()
    {
        base.Start();
        Init();
    }

    public override float TakeDamage(float value)
    {

        return base.TakeDamage(value);
    }
    public override void Die()
    {
        base.Die();
        this._animator.SetTrigger("die");
    }


    public void Move()
    {
        this._animator.SetBool(walkingBoolName, true);
    }
    public void StopMoving()
    {
        this._animator.SetBool(walkingBoolName, false);
    }

    /// <summary>
    /// Metodo para animation
    /// </summary>
    public void Attack()
    {
        this.attackArea.enabled = !this.attackArea.enabled;
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
