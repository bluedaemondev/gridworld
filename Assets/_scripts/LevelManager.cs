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

    public float timeTicker = 30;

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

    }

}
