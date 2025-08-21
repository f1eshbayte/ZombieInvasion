using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private float _durationAfterDeath;

    [SerializeField] private Player _target;

    public event UnityAction Dying;
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    private void DetectDie()
    {
        if (_health <= 0)
        {
            Dying?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
