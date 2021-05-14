using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAttackerProxy : MonoBehaviour
{
    [SerializeField]
    private EnemyShooterBehaviour controlledBehaviour;

    public void Attack()
    {
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.attackSoundShooter);

        controlledBehaviour.CreateProjectile();
    }
}