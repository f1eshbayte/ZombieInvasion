using System.Collections.Generic;
using UnityEngine;
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
            if(item is DoorUpgradeItem doorItem)
                _container.Inject(doorItem);
            
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
    }

    private void OnSellButtonClick(IShopItem item, ItemView view)
    {
        TrySellItem(item, view);
    }

    private void TrySellItem(IShopItem item, ItemView view)
    {
        if (item is DoorUpgradeItem doorItem)
        {
            if (doorItem.TryBuy(_player))
            {
                view.Render(item);
                if (doorItem.Price == 0)
                    view.DisableButton();
            }
        }
        else if (item.Price <= _player.Money)
        {
            _player.BuyItem(item);
            if (item is Weapon weapon && weapon.IsBuyed == false)
            {
                weapon.Buy();
                view.SellButtonClick -= OnSellButtonClick;
            }
        }
    }
}