using System;
using UnityEngine;
using UnityEngine.Events;
 
[RequireComponent(typeof(EnemyStateMachine))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _reward;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _soundDying;
    [SerializeField] private AudioClip _soundHit;
    
    private int _currentHealth;
    private EnemyStateMachine _stateMachine;
    public Player TargetPlayer { get; private set; }
    public Door TargetDoor { get; private set; }
    public int Reward => _reward;

    public event UnityAction<Enemy> Dying;

    private void Awake()
    {
        _stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnDisable()
    {
        _stateMachine.ResetStateMachine();
    }

    public void Init(Player targetPlayer, Door targetDoor)
    {
        TargetPlayer = targetPlayer;
        TargetDoor = targetDoor;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _audioSource.PlayOneShot(_soundHit);
        DetectDie();
    }
    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    public void ResetEnemy()
    {
        _currentHealth = _maxHealth;
        _stateMachine.ResetStateMachine();
    }

    private void DetectDie()
    {
        if (_currentHealth <= 0)
        {
            _audioSource.PlayOneShot(_soundDying);
            Dying?.Invoke(this);
        }
    }
}