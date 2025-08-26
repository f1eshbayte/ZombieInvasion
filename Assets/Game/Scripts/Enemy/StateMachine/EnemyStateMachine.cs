using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    
    // private Player _targetPlayer;
    // private Door _targetDoor;
    private State _currentState;
    private Enemy _enemy;

    // public State Current => _currentState;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    // private void Start()
    // {
    //     _targetPlayer = GetComponent<Enemy>().TargetPlayer;
    //     _targetDoor = GetComponent<Enemy>().TargetDoor;
    //     Reset(_firstState);
    // }
    

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if(nextState != null)
            Transit(nextState);
    }

    public void ResetStateMachine()
    {
        if (_currentState != null)
            _currentState.Exit(); // отключаем старое состояние
        Transit(_firstState);
    }
    
    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;
        
        if(_currentState != null)
            _currentState.Enter(_enemy.TargetPlayer, _enemy.TargetDoor);

    }
}
