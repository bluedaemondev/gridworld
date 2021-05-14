using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAttackerProxy : MonoBehaviour
{
    [SerializeField]
    private EnemyShooterBehaviour controlledBehaviour;

    public void Attack()
    {
        controlledBehaviour.CreateProjectile();
    }
}