using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    
    private Player _targetPlayer;
    private Door _targetDoor;
    private State _currentState;

    public State Current => _currentState;

    private void Start()
    {
        _targetPlayer = GetComponent<Enemy>().TargetPlayer;
        _targetDoor = GetComponent<Enemy>().TargetDoor;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if(nextState != null)
            Transit(nextState);
            
    }

    private void Reset(State startState)
    {
        _currentState = startState;
        if(_currentState != null)
            _currentState.Enter(_targetPlayer, _targetDoor);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;
        
        if(_currentState != null)
            _currentState.Enter(_targetPlayer, _targetDoor);
    }
}
