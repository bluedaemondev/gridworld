using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Guarda todos los clips de audio que parezcan necesarios para la escena
/// </summary>
[CreateAssetMenu(fileName ="Sound Library", menuName ="Sound Library", order = 0)]
public class SoundLibrarySO : ScriptableObject
{
    public AudioClip musicLevelLoop;

    public AudioClip attackSoundPlayer;
    public AudioClip explosionSound;
    public AudioClip stepPlayer;
    public AudioClip spawnEnemy;

    public AudioClip winSound;
    public AudioClip loseSound;

}
