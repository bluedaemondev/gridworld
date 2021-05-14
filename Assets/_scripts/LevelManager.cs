using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public EntityLibrarySO assets;

    public Transform enemiesContainer;
    public Transform player;


    public MapManager mapManager;
    public Cronometer cronometer;
    public Managers.SceneManager sceneManager;
    public Managers.HudManager hudManager;

    public float timeTicker = 30;

    public event Action onLose;
    public event Action onWin;

    List<Entity> aliveEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;


        if (!player)
            player = FindObjectOfType<Entities.Player>().transform;
        if (!mapManager)
            mapManager = FindObjectOfType<MapManager>();
        
        mapManager.Init();
        cronometer.Init(timeTicker);

        onLose += RestartOnLose;
    }
    public void Win()
    {
        if(onWin != null && aliveEnemies.Count == 0)
        {
            hudManager.DisplayWin();
            onWin.Invoke();
        }
    }

    public void Lose()
    {
        if(onLose != null)
        {
            hudManager.DisplayLose();
            onLose.Invoke();
        }
    }

    void RestartOnLose()
    {
        StartCoroutine(RestartSceneTimed(3f));
    }
    IEnumerator RestartSceneTimed(float t)
    {
        yield return new WaitForSeconds(t);
        sceneManager.ReLoadSync();
    }
}
