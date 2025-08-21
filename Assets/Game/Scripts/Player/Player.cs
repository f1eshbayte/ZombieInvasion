using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private List<Weapon> _weapons;

    private int _currentHealth;
    private Weapon _currentWeapon;

    public float Speed => _speed;
    public Weapon CurrentWeapon => _currentWeapon;
    public int Money { get; private set; }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentWeapon = _weapons[0];
    }

    public void TakeDamage(int damage)
    {
        if (!IsDied())
            _currentHealth -= damage;
    }

    public void EnemyDied(int reward)
    {
        Money += reward;
    }

    private bool IsDied()
    {
        return _currentHealth <= 0;
    }
}
