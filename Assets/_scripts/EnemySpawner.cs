using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static int maxEnemiesPool = 20;
    private static int enemiesInPool = 0;

    [SerializeField] private float spawnAfter = 3f;
    public void SpawnEnemy()
    {
        var nEnemy = Enemy.InstantiateEnemy(transform.position, Quaternion.identity, LevelManager.instance.enemiesContainer);
        nEnemy.Init();
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
            Enemy.InstantiateEnemy(transform.position, Quaternion.identity, LevelManager.instance.enemiesContainer);
            yield return new WaitForSecondsRealtime(spawnAfter);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
