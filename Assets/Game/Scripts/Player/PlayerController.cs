using UnityEngine;
using Zenject;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] int _maxLimitPositionX = 6;
    [SerializeField] int _minLimitPositionX = -6;

    private Player _player;
    private PlayerVisual _playerVisual;
    private PlayerInput _input;

    private Vector2 _direction;
    private bool _moveLeft;
    private bool _moveRight;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerVisual = GetComponent<PlayerVisual>();
        _input = new PlayerInput();

        _input.Player.Shoot.performed += ctx => _signalBus.Fire(new ShootSignal(true)); 
        _input.Player.Shoot.canceled += ctx => _signalBus.Fire(new ShootSignal(false)); 
    }

    private void OnEnable()
    {
        _input.Enable();

        _signalBus.Subscribe<ShootSignal>(OnShootButton);
        _signalBus.Subscribe<MoveLeftSignal>(OnMoveLeft);
        _signalBus.Subscribe<MoveRightSignal>(OnMoveRight);
    }

    private void OnDisable()
    {
        _input.Disable();

        _signalBus.Unsubscribe<ShootSignal>(OnShootButton);
        _signalBus.Unsubscribe<MoveLeftSignal>(OnMoveLeft);
        _signalBus.Unsubscribe<MoveRightSignal>(OnMoveRight);
    }

    private void Update()
    {
        var mobileDir = MobileMove();
        var keyboardDir = PCMove();

        SetPriority(mobileDir, keyboardDir);

        Move(_direction);
    }

    private void SetPriority(Vector2 mobileDir, Vector2 keyboardDir)
    {
        _direction = mobileDir != Vector2.zero ? mobileDir : keyboardDir;
    }

    private Vector2 PCMove()
    {
        Vector2 keyboardDir = _input.Player.Move.ReadValue<Vector2>();
        return keyboardDir;
    }

    private Vector2 MobileMove()
    {
        Vector2 mobileDir = Vector2.zero;
        if (_moveLeft)
            mobileDir = Vector2.left;
        if (_moveRight)
            mobileDir = Vector2.right;
        return mobileDir;
    }


    private void OnMoveLeft(MoveLeftSignal signal)
    {
        _moveLeft = signal.IsDown;
    }

    private void OnMoveRight(MoveRightSignal signal)
    {
        _moveRight = signal.IsDown;
    }

    private void OnShootButton(ShootSignal signal)
    {
        _player.CurrentWeapon.HandleShooting(_raycastPoint, _playerVisual, signal.IsDown);
    }

    private void Move(Vector2 direction)
    {
        // left
        if (direction.x < 0 && transform.position.x <= _minLimitPositionX)
            direction.x = 0;
        // right
        if (direction.x > 0 && transform.position.x >= _maxLimitPositionX)
            direction.x = 0;

        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);


        transform.position += new Vector3(direction.x * _player.Speed * Time.deltaTime, 0, 0);
        _playerVisual.PlayWalk(Mathf.Abs(direction.x));
    }
}