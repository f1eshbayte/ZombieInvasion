public class TargetDieTransition : Transition
{
    private void Update()
    {
        if (!TargetPlayer.isActiveAndEnabled)
        {
            NeedTransit = true;
        }
    }
}