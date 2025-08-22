using UnityEngine;

public class AttackDoorState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    
    private float _lastAttackTime;
    
    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(TargetDoor);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Door target)
    {
        target.TakeDamage(_damage);
    }
}
