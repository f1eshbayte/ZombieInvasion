using UnityEngine;

public class DoorTransition : Transition
{

    [SerializeField] private DoorTransitionType _transitionType;
    
    private Collider2D _doorCollider;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Door door))
            _doorCollider = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Door door))
            _doorCollider = null;
    }

    private void Update()
    {
        bool inDoorZone = _doorCollider != null && _doorCollider.enabled;
        
        switch (_transitionType)
        {
            case DoorTransitionType.Enter:
                if (inDoorZone)
                {
                    NeedTransit = true;
                }
                break;
            case DoorTransitionType.Exit:
                if (!inDoorZone)
                {
                    NeedTransit = true;
                }
                break;
        }
    }
}
