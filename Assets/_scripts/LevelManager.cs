using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public EntityLibrarySO assets;

    public Transform enemiesContainer;
    public Transform player;


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
    }

    
}
