using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;
    
    protected Player TargetPlayer { get; private set; }
    protected Door TargetDoor { get; private set; }

    public State TargetState => _targetState;
    
    public bool NeedTransit { get; protected set; }

    public void Init(Player targetPlayer, Door targetDoor)
    {
        TargetPlayer = targetPlayer;
        TargetDoor = targetDoor;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
