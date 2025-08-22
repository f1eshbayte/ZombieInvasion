using UnityEngine;

public class BackToWalkTransition : Transition
{
    [SerializeField] private float _exitAttackRange;

    private void Update()
    {
        if (Vector2.Distance(transform.position, TargetPlayer.transform.position) > _exitAttackRange)
            NeedTransit = true;
    }
}
