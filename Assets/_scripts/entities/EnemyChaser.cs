using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chasing enemy
/// </summary>
public class EnemyChaser : Entity, IAttacker, IMovable, IRagdoll
{
    public static EnemyChaser InstantiateEnemy(Vector3 position, Quaternion rot, Transform enemyContainerScene, EnemySpawner spawnPoint)
    {
        var go = Instantiate(LevelManager.instance.assets.enemyChaser_Prefab, position, rot, enemyContainerScene);
        var result = go.GetComponent<EnemyChaser>();
        result._originSpawner = spawnPoint;
        return result;
    }

    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider attackArea;

    [SerializeField] private TargetChaser _chaser;
    [SerializeField] private EnemySpawner _originSpawner;

    [SerializeField] private string walkingBoolName = "isWalking";
    [SerializeField] private string damagedAnimationName = "Damaged";
    [SerializeField] private string attackAnimationName = "Attack";

    public bool canMove = true;

    public void Init()
    {
        this._health = new Health(50);

        if (attackArea != null)
            this.attackArea.enabled = false;

        if (_chaser == null)
            _chaser = GetComponent<TargetChaser>();

        if (_animator == null)
            _animator = GetComponent<Animator>();


        //DisableRagdollPhysics();

        LevelManager.instance.SubscribeAliveEntity(this);

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
        if (!this.IsDead())
        {
            value = base.TakeDamage(value);

            this._animator.Play(damagedAnimationName);

            this._myRigidbody.AddExplosionForce(3 * value / 4, transform.position, 2);
        }
        return value;
    }
    public override void Die()
    {
        base.Die();
        this._animator.SetTrigger("die");
        LevelManager.instance.RemoveEnemyFromAccountance(this);


        EnableRagdollPhysics();
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
    /// metodo para animacion
    /// </summary>
    public void AttackEvent()
    {
        if (!_health.IsDead())
            this.attackArea.enabled = !this.attackArea.enabled;
        else
            this.attackArea.enabled = false;
    }
    public void SwitchCanMove()
    {
        this.canMove = !this.canMove;
    }

    public void Attack()
    {
        if (!this._health.IsDead())
            //this._animator.SetBool(attackBoolParam, true);
            this._animator.Play(attackAnimationName);
    }
    public void EnableRagdollPhysics()
    {

        if (_myRigidbody != null)
            _myRigidbody.isKinematic = false;
        
        _animator.enabled = false;

        Component[] components = GetComponentsInChildren(typeof(Transform));

        foreach (Component c in components)
        {
            if (c.TryGetComponent<Rigidbody>(out Rigidbody rb) && rb != this.transform)
            {
                rb.isKinematic = false;
            }
        }

    }

    public void DisableRagdollPhysics()
    {
        if (_myRigidbody != null)
            _myRigidbody.isKinematic = true;

        _animator.enabled = false;

        Component[] components = GetComponentsInChildren(typeof(Transform));

        foreach (Component c in components)
        {
            if (c.TryGetComponent<Rigidbody>(out Rigidbody rb) && rb != this.transform)
            {
                rb.isKinematic = true;
            }
        }
    }
}
