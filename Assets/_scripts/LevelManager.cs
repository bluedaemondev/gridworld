using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public EntityLibrarySO assets;

    public Transform enemiesContainer;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    
}
