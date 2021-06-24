using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummonerAttackerProxy : MonoBehaviour
{
    [SerializeField]
    private EnemySummonerBehaviour controlledBehaviour;

    public void Attack()
    {
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.attackSoundShooter);

        controlledBehaviour.SummonEnemy();
    }
}