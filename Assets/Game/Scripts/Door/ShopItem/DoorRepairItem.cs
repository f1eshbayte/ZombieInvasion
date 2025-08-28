using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DoorRepairItem : MonoBehaviour, IShopItem
{
    [SerializeField] private Sprite _icon; // одна иконка (например молоток/ремонт)

    private DoorService _service;
    private DoorUpgrade _leftDoor;
    private int _price;

    public event UnityAction OnPriceChanged;
    
    public string Label => "Починить двери";
    public int Price => _price;
    public Sprite Icon => _icon;

    [Inject]
    public void Construct(DoorService service, [Inject(Id = DoorID.LeftDoor)] DoorUpgrade leftDoor)
    {
        _service = service;
        _leftDoor = leftDoor;

        // подписываемся на событие DoorService
        _service.OnDoorsUpgraded += UpdatePrice;

        UpdatePrice(); // сразу обновляем цену при старте
    }

    private void OnDestroy()
    {
        if (_service != null)
            _service.OnDoorsUpgraded -= UpdatePrice;
    }

    public bool TryBuy(Player player)
    {
        UpdatePrice();
        if (player.Money >= Price)
        {
            player.BuyItem(this);
            _service.RepairBothDoors();
            return true;
        }

        return false;
    }

    private void UpdatePrice()
    {
        if (_leftDoor == null) 
            return;
        
        var currentDoor = _leftDoor.GetCurrentDoorData();
        _price = currentDoor.RepairPrice;
        
        OnPriceChanged?.Invoke();
    }
}