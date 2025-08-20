using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Shoot }
    public ButtonType buttonType;

    private PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>(); // ваще пиздец надо как с эти Player'ом решать как сделать через zenject 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_player == null) return;

        switch (buttonType)
        {
            case ButtonType.Left:
                _player.MoveLeftDown();
                break;
            case ButtonType.Right:
                _player.MoveRightDown();
                break;
            case ButtonType.Shoot:
                _player.OnShootButton();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_player == null) return;

        switch (buttonType)
        {
            case ButtonType.Left:
                _player.MoveLeftUp();
                break;
            case ButtonType.Right:
                _player.MoveRightUp();
                break;
        }
    }
}