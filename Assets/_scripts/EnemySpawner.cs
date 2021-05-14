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
            yield return new WaitForSecondsRealtime(spawnAfter);
        }
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(SpawnCyclic());
    }

}
