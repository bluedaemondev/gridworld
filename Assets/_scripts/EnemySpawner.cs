using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Entity
{
    private static int maxEnemiesPool = 20;
    private static int enemiesInPool = 0;

    [SerializeField] private float spawnAfter = 3f;
    
    public void SpawnEnemy()
    {
        var nEnemy = EnemyChaser.InstantiateEnemy(transform.position, Quaternion.identity, LevelManager.instance.enemiesContainer);
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
