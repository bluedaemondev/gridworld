using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Chaser,
    Shooter,
}
public class EnemySpawner : Entity
{
    private static int maxEnemiesPool = 20;
    private static int enemiesInPool = 0;

    [SerializeField] private float spawnAfter = 3f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string spawnAnimationName = "OnSpawn";
    [SerializeField] private string damageAnimationName = "OnDamage";
    [SerializeField] private string dyingAnimationName = "DyingSpawner";



    public EnemyType toSpawn = EnemyType.Chaser;
    
    public void SpawnEnemy()
    {

        var nEnemy = EnemyChaser.InstantiateEnemy(transform.position, Quaternion.identity, LevelManager.instance.enemiesContainer, this);
        
        // pendiente para tener el otro enemigo y la clase base

        //switch (toSpawn) {
        //    case EnemyType.Chaser:
        //        break;
        //    case EnemyType.Shooter:
        //        break;
        //}

        nEnemy.Init();
        enemiesInPool++;

    }

    public void RemoveEnemy()
    {
        if (enemiesInPool > 0)
            enemiesInPool--;
    }

    private IEnumerator SpawnCyclic()
    {
        while(enemiesInPool < maxEnemiesPool)
        {
            SpawnEnemy();
            _animator.Play(spawnAnimationName);

            yield return new WaitForSecondsRealtime(spawnAfter);
        }
    }

    public override void Start()
    {
        Init();
        base.Start();
    }
    public override void Destroy()
    {
        base.Destroy();
    }
    private void Init()
    {
        this._health = new Health(400);
        this._animator = GetComponent<Animator>();
        StartCoroutine(SpawnCyclic());
    }
    public override float TakeDamage(float value)
    {
        value = base.TakeDamage(value);
        this._animator.Play(damageAnimationName);

        return value;
    }
    public override void Die()
    {
        base.Die();
        this._animator.SetTrigger("die");
        this._animator.Play(dyingAnimationName);
    }
}
