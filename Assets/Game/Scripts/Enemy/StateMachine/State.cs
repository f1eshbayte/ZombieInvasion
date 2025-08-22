using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player TargetPlayer { get; private set; }
    protected Door TargetDoor { get; private set; }

    public void Enter(Player targetPlayer, Door targetDoor)
    {
        if (enabled == false)
        {
            TargetPlayer = targetPlayer;
            TargetDoor = targetDoor;
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(TargetPlayer, TargetDoor);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;
            
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}