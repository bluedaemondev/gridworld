using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IState
{
    private StateMachine _fsm;
    private EnemyGuard _enemy;

    public PatrollingState(StateMachine fsm, EnemyGuard p)
    {
        _fsm = fsm;
        _enemy = p;
    }

    public void OnStart()
    {
        _enemy.Move();
        Debug.Log("Entré a Patrol");
    }
    public void OnUpdate()
    {
        _enemy.Patrol();

        _enemy.FieldOfView();

        if(_enemy.foundTarget)
        {
            _fsm.ChangeState(GuardStatesEnum.Pursuit);
        }
    }
    public void OnExit()
    {
        Debug.Log("Sali de Patrol");
    }
}
