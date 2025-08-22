using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int, int> HealthChanged; 
    

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentWeapon = _weapons[0];
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (IsDied())
        {
            Death();
        }
    }

    public void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private bool IsDied()
    {
        return _currentHealth <= 0;
    }
}