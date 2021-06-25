using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Guarda todos los clips de audio que parezcan necesarios para la escena
/// </summary>
[CreateAssetMenu(fileName ="Entity Library", menuName ="Entity Library", order = 1)]

public class EntityLibrarySO : ScriptableObject
{
    public GameObject enemyChaser_Prefab;
    public GameObject spawner_Prefab;
    public GameObject explodingBarrel_Prefab;
    public GameObject enemyShooter_Prefab;
    public GameObject enemySummoner_Prefab;
    public GameObject enemyBomber_Prefab;


}
