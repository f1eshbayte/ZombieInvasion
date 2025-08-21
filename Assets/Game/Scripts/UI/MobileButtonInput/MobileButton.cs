using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Shoot }
    public ButtonType buttonType;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                _signalBus.Fire(new MoveLeftSignal(true));
                break;
            case ButtonType.Right:
                _signalBus.Fire(new MoveRightSignal(true));
                break;
            case ButtonType.Shoot:
                _signalBus.Fire(new ShootSignal(true));
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                _signalBus.Fire(new MoveLeftSignal(false));
                break;
            case ButtonType.Right:
                _signalBus.Fire(new MoveRightSignal(false));
                break;
            case ButtonType.Shoot:
                _signalBus.Fire(new ShootSignal(false));
                break;
        }
    }
}