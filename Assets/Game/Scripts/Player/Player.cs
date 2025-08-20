using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;

    private int _currentHealth;

    public float Speed => _speed;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!IsDied())
            _currentHealth -= damage;
    }

    private bool IsDied()
    {
        return _currentHealth <= 0;
    }
}
