using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public EntityLibrarySO assets;
    public SoundLibrarySO soundAssets;

    public Transform enemiesContainer;
    public Transform player;


    public MapManager mapManager;
    public Cronometer cronometer;
    public Managers.SceneManager sceneManager;
    public Managers.HudManager hudManager;

    public float timeTicker = 30;

    public event Action onLose;
    public event Action onWin;

    [SerializeField] private List<Entity> aliveEnemies;
    [SerializeField] private int countMaxEnemies;

    /// <summary>
    /// win condition - destroy everyone
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveEnemyFromAccountance(Entity entity)
    {
        if (aliveEnemies.Contains(entity))
        {
            Debug.Log("removing " + entity.name);
            aliveEnemies.Remove(entity);

            hudManager.UpdateUIEnemies(aliveEnemies.Count, countMaxEnemies);


            if (aliveEnemies.Count == 0)
            {
                Win();
            }
        }
    }
    public void SubscribeAliveEntity(Entity entity)
    {
        if (!aliveEnemies.Contains(entity))
        {
            Debug.Log("adding " + entity.name);
            aliveEnemies.Add(entity);

            hudManager.UpdateUIEnemies(aliveEnemies.Count, ++countMaxEnemies);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;


        
    }
    private void Start()
    {
        if (!player)
            player = FindObjectOfType<Entities.Player>().transform;
        if (!mapManager)
            mapManager = FindObjectOfType<MapManager>();
        if (aliveEnemies == null)
            aliveEnemies = new List<Entity>();

        mapManager.Init();
        cronometer.Init(timeTicker);

        

        onLose += RestartOnLose;
        onWin += PauseGame;
    }
    public void Win()
    {
        if(onWin != null)
        {
            Debug.Log("Win!");
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
    public void PauseGame()
    {
        if (Time.timeScale == 0.1f)
            Time.timeScale = 1;
        else
            Time.timeScale = 0.1f;
    }
    public void SetToTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
