using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private DoorID doorId;

    private DoorData _data;
    private DoorUpgrade _upgradeManager;
    private BoxCollider2D _boxCollider2D;

    public int CurrentHealth { get; private set; }
    private int _currentStageIndex = -1;
    private bool _isOpen = false;

    public event UnityAction<int> OnDoorStageChanged;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        CurrentHealth = _data.MaxHealth;
        UpdateDoorVisual();
    }

    [Inject]
    public void Construct(DiContainer container)
    {
        _data = container.ResolveId<DoorData>(doorId);
        _upgradeManager = container.ResolveId<DoorUpgrade>(doorId);
        _upgradeManager.RegisterDoor(this);
    }

    public void TakeDamage(int amount)
    {
        if (IsBroken())
            return;
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        PlaySound(_data.DamageSound);

        UpdateDoorVisual();
    }

    public void Repair()
    {
        CurrentHealth = _data.MaxHealth;
        _boxCollider2D.enabled = true;
        Close();
        UpdateDoorVisual();
    }

    public void SetState()
    {
        if (IsBroken())
            return;

        if (_isOpen)
            Close();
        else
            Open();
    }

    public void SetData(DoorData newData)
    {
        if (newData == null)
        {
            return;
        }

        _data = newData;
        CurrentHealth = _data.MaxHealth;
        _currentStageIndex = -1;
        UpdateDoorVisual();
    }
    


    private void Open()
    {
        if (_isOpen)
            return;

        gameObject.transform.localRotation = Quaternion.Euler(0, -180, 0);
        _boxCollider2D.enabled = false;
        _isOpen = true;
        PlaySound(_data.OpenSound);
    }

    private void Close()
    {
        if (!_isOpen)
            return;

        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _boxCollider2D.enabled = true;

        _isOpen = false;
        PlaySound(_data.CloseSound);
    }

    private bool IsBroken()
    {
        if (CurrentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            return true;
        }

        return false;
    }

    private void UpdateDoorVisual()
    {
        int newStageIndex = 0;

        for (int i = 0; i < _data.Stages.Count; i++)
        {
            if (CurrentHealth <= _data.Stages[i].Threshold)
                newStageIndex = i;
        }

        if (newStageIndex != _currentStageIndex)
        {
            _currentStageIndex = newStageIndex;
            _spriteRenderer.sprite = _data.Stages[_currentStageIndex].Sprite;

            var stage = _data.Stages[_currentStageIndex];
            if (stage.ThresholdSound != null)
                PlaySound(stage.ThresholdSound);

            OnDoorStageChanged?.Invoke(_currentStageIndex);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            _audioSource.PlayOneShot(clip);
    }
}