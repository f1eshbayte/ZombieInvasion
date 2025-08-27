using System;
using UnityEngine;
using Zenject;

public class DoorUpgradeItem : MonoBehaviour, IShopItem
{
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private int[] _prices;
    
    private DoorUpgradeService _service;
    [NonSerialized] private int _currentUpgradeIndex = 0;
    
    public string Label => GetLabel();
    public int Price => GetPrice();
    public Sprite Icon => GetIcon();

    [Inject]
    public void Construct(DoorUpgradeService service)
    {
        _service = service;
    }

    public bool TryBuy(Player player)
    {
        if (_currentUpgradeIndex >= _prices.Length)
            return false;

        if (player.Money >= Price)
        {
            player.BuyItem(this);
            _service.UpgradeBothDoors();
            _currentUpgradeIndex++;
            return true;
        }

        return false;
    }
    
    private string GetLabel()
    {
        return _currentUpgradeIndex >= _prices.Length ? "Максимальный уровень" : $"Улучшить двери (уровень {_currentUpgradeIndex + 2})";
    }
    private int GetPrice()
    {
        return _currentUpgradeIndex >= _prices.Length ? 0 : _prices[_currentUpgradeIndex];
    }
    
    private Sprite GetIcon()
    {
        return _currentUpgradeIndex >= _prices.Length ? _icons[_icons.Length-1] : _icons[_currentUpgradeIndex];
    }
    
    


   
    
    
}
