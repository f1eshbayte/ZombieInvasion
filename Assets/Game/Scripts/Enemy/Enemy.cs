using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private float _durationAfterDeath;

    public Player TargetPlayer { get; private set; }
    public Door TargetDoor { get; private set; }
    public int Reward => _reward;
    
    public event UnityAction<Enemy> Dying;

    public void Init(Player targetPlayer, Door targetDoor)
    {
        TargetPlayer = targetPlayer;
        TargetDoor = targetDoor;
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        DetectDie();
    }

    private void DetectDie()
    {
        if (_health <= 0)
        {
            // Dying?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
