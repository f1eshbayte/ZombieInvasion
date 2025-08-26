using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DieTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
        _enemy.Dying += OnEnemyDying;
    }
    
    private void OnDisable()
    {
        _enemy.Dying -= OnEnemyDying;
    }

    private void OnEnemyDying(Enemy _)
    {
        NeedTransit = true;
    }
}
