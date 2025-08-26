using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DiedState : State
{
    private Animator _animator;

    private const string DieAnimate = "Die";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(DieAnimate);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
