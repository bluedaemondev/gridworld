using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    private StateMachine _fsm;
    private EnemyGuard _enemy;
    private float _timer;
    
    public DieState(StateMachine fsm, EnemyGuard p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        Debug.Log("Entré a Die");
    }

    public void OnUpdate()
    {
        _enemy.DeactivateNavMesh();
        _enemy.Die();
    }
    
    public void OnExit()
    {
        Debug.Log("Salí de Die");
    }
}
