using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private const string Shoot = "Shoot";
    private const string Speed = "Speed";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayShoot()
    {
        _animator.SetTrigger(Shoot);
    }

    public void PlayWalk(float value)
    {
        _animator.SetFloat(Speed, value);
    }
}
