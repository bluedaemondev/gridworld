using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuingState : IState
{
    private StateMachine _fsm;
    private EnemyGuard _enemy;
    private float _timer;
    
    public PursuingState(StateMachine fsm, EnemyGuard p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        _enemy.Run();
        Debug.Log("Entré a Pursuit");
    }

    public void OnUpdate()
    {
        _enemy.BeginChase();
    }
    
    public void OnExit()
    {
        Debug.Log("Salí de Pursuit");
    }
}
