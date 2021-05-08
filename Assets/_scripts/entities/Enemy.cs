using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chasing enemy
/// </summary>
public class Enemy : Entity, IAttacker, IMovable
{
    public static Enemy InstantiateEnemy(Vector3 position, Quaternion rot, Transform enemyContainerScene)
    {
        var result = Instantiate(LevelManager.instance.assets.enemyChaser_Prefab, position, rot, enemyContainerScene);
        return result.GetComponent<Enemy>();
    }

    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider attackArea;

    [SerializeField] private TargetChaser _chaser;

    public void Init()
    {
        this._health = new Health(50);

        if (attackArea != null)
            this.attackArea.enabled = false;

        if (_chaser == null)
            _chaser = GetComponent<TargetChaser>();
        

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


    public void Move()
    {
        // refference chase behaviour
        throw new NotImplementedException();
    }

    /// <summary>
    /// Metodo para animation
    /// </summary>
    public void Attack()
    {
        this.attackArea.enabled = !this.attackArea.enabled;
    }

}
