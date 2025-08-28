using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _items;
    [SerializeField] private ItemView _template;
    [SerializeField] private GameObject _itemContainer;

    private Player _player;
    private DiContainer _container;

    [Inject]
    public void Construct(Player player, DiContainer container)
    {
        _player = player;
        _container = container;
    }

    private void Start()
    {
        foreach (var item in _items)
        {
            if(item is DoorUpgradeItem doorItem || item is DoorRepairItem)
                _container.Inject(item);
            
            if (item is IShopItem shopItem)
                AddItem(shopItem);
            else
                Debug.LogWarning($"{item.name} не реализует IShopItem!");
        }
    }

    
    private void AddItem(IShopItem item)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(item);

        if (item is DoorRepairItem repairItem)
        {
            // сохраняем делегат, чтобы потом можно было отписаться
            UnityAction updateUI = () => view.Render(repairItem);
            repairItem.OnPriceChanged += updateUI;

            // отписка при уничтожении ItemView
            view.OnDestroyed += () => repairItem.OnPriceChanged -= updateUI;
        }
    }

    private void OnSellButtonClick(IShopItem item, ItemView view)
    {
        TrySellItem(item, view);
    }

    private void TrySellItem(IShopItem item, ItemView view)
    {
        if (TrySellDoorUpgrade(item, view)) 
            return;
        if (TrySellDoorRepair(item, view)) 
            return;
        
        TrySellGenericItem(item, view);
    }

    private bool TrySellDoorUpgrade(IShopItem item, ItemView view)
    {
        if (item is not DoorUpgradeItem doorItem) 
            return false;

        if (doorItem.TryBuy(_player))
        {
            view.Render(item);
            if (doorItem.Price == 0)
                view.DisableButton();
        }

        return true;
    }

    private bool TrySellDoorRepair(IShopItem item, ItemView view)
    {
        if (item is not DoorRepairItem repairItem) 
            return false;

        if (repairItem.TryBuy(_player))
            view.Render(item); // обновляем цену починки

        return true;
    }

    private void TrySellGenericItem(IShopItem item, ItemView view)
    {
        if (item.Price > _player.Money) 
            return;

        _player.BuyItem(item);

        if (item is Weapon weapon && !weapon.IsBuyed)
        {
            weapon.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

}