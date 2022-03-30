using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState _currentState = new BlankState();
    Dictionary<GuardStatesEnum, IState> _allStates = new Dictionary<GuardStatesEnum, IState>();

    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public void ChangeState(GuardStatesEnum id)
    {
        if (!_allStates.ContainsKey(id)) return;

        _currentState.OnExit();
        _currentState = _allStates[id];
        _currentState.OnStart();

    }

    public void AddState(GuardStatesEnum id, IState state)
    {
        if (_allStates.ContainsKey(id)) return;
        _allStates.Add(id, state);
    }
}

public enum GuardStatesEnum
{
    Die,
    Patrol,
    Pursuit
}
