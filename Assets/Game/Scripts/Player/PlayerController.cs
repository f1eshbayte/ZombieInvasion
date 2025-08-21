// using UnityEngine;
//
// [RequireComponent(typeof(Player))]
// public class PlayerController : MonoBehaviour
// {
//     [SerializeField] private Transform _raycastPoint;
//     private Player _player;
//     private PlayerInput _input;
//     private PlayerVisual _playerVisual;
//     private Vector2 _direction;
//
//     private void Awake()
//     {
//         _player = GetComponent<Player>();
//         _input = new PlayerInput();
//         _playerVisual = GetComponent<PlayerVisual>();
//         _input.Player.Shoot.performed += ctx => OnShoot();
//     }
//
//     private void Update()
//     {
//         _direction = _input.Player.Move.ReadValue<Vector2>();
//         
//         Move(_direction);
//     }
//     
//     private void OnEnable()
//     {
//         _input.Enable();
//     }
//
//     private void OnDisable()
//     {
//         _input.Disable();
//     }
//
//     private void OnShoot()
//     {
//         Debug.Log("Shoot!");
//         Physics2D.Raycast(_raycastPoint.position, Vector2.right);
//         
//         _playerVisual.PlayShoot();
//     }
//
//     private void Move(Vector2 direction)
//     {
//         if (direction.x < 0)
//             transform.rotation = Quaternion.Euler(0, 180, 0);
//         else if(direction.x > 0)
//             transform.rotation = Quaternion.Euler(0, 0, 0);
//             
//         transform.position += new Vector3(direction.x * _player.Speed * Time.deltaTime, 0, 0);
//         
//         _playerVisual.PlayWalk(Mathf.Abs(direction.x));
//     }
// }

using UnityEngine;
using Zenject;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint; // выстрел будущее
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

        // // ПК стреляет
        // _input.Player.Shoot.performed += ctx => OnShootButton();
        _input.Player.Shoot.performed += ctx => _signalBus.Fire(new ShootSignal(true));   // Нажатие
        _input.Player.Shoot.canceled  += ctx => _signalBus.Fire(new ShootSignal(false)); // Отпускание

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
        // ===== Мобильное управление =====
        Vector2 mobileDir = Vector2.zero;
        if (_moveLeft)
            mobileDir = Vector2.left;
        if (_moveRight)
            mobileDir = Vector2.right;

        // ===== ПК управление =====
        Vector2 keyboardDir = _input.Player.Move.ReadValue<Vector2>();

        // Если есть мобильное управление, оно имеет приоритет
        _direction = mobileDir != Vector2.zero ? mobileDir : keyboardDir;

        Move(_direction);
    }


    // ===== Signals =====
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
        Debug.Log("Shoot  ИЗ КОНТРОЛЛЕРА!");
        _player.CurrentWeapon.HandleShooting(_raycastPoint, _playerVisual, signal.IsDown);
    }

    // ===== Движение =====
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