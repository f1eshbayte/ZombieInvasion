using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private DoorID doorId;
    
    private DoorData _data;

    private DoorUpgrade _upgradeManager;

    public int CurrentHealth { get; private set; }
    private int _currentStageIndex = -1;
    private bool _isOpen = false;

    public event UnityAction<int> OnDoorStageChanged;

    private void Start()
    {
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
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        if (CurrentHealth > 0)
            PlaySound(_data.damageSound);

        UpdateDoorVisual();
    }

    public void Repair(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, _data.MaxHealth);
        UpdateDoorVisual();
    }

    public void SetState()
    {
        if (_isOpen)
            Close();
        else
            Open();
    } 

    private void Open()
    {
        if (_isOpen) 
            return;
        
        gameObject.transform.localRotation = Quaternion.Euler(0, -180, 0);
        Debug.Log("дверь открылась");
        _isOpen = true;
        PlaySound(_data.openSound);
    }

    private void Close()
    {
        if (!_isOpen) 
            return;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        Debug.Log("дверь закрылась");
        _isOpen = false;
        PlaySound(_data.closeSound);
    }
    public void SetData(DoorData newData)
    {
        if (newData == null)
        {
            Debug.LogError("DoorData is null!");
            return;
        }

        _data = newData;
        CurrentHealth = _data.MaxHealth;
        _currentStageIndex = -1;
        UpdateDoorVisual();
    }

    private void UpdateDoorVisual()
    {
        int newStageIndex = 0;

        for (int i = 0; i < _data.stages.Count; i++)
        {
            if (CurrentHealth <= _data.stages[i].threshold)
                newStageIndex = i;
        }

        if (newStageIndex != _currentStageIndex)
        {
            _currentStageIndex = newStageIndex;
            _spriteRenderer.sprite = _data.stages[_currentStageIndex].sprite;

            var stage = _data.stages[_currentStageIndex];
            if (stage.thresholdSound != null)
                PlaySound(stage.thresholdSound);

            OnDoorStageChanged?.Invoke(_currentStageIndex);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            _audioSource.PlayOneShot(clip);
    }
}
