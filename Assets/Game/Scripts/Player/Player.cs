using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private List<Weapon> _weapons;

    private int _currentHealth;
    private Weapon _currentWeapon;
    private DoorUpgradeItem _doorUpgradeItem;
    public int AidKitCount { get; private set; }
    public int Money { get; private set; } = 50;
    public int Fragments { get; private set; } = 50;
    public float Speed => _speed;
    public Weapon CurrentWeapon => _currentWeapon;
    
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int> AidKitChanged;
    public event UnityAction<int> FragmentsChanged;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> DamageTaken;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentWeapon = _weapons[0];
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        DamageTaken?.Invoke(damage);
        if (IsDied())
        {
            Death();
        }
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void Heal(int healCount)
    {
        if(AidKitCount <= 0)
            return;

        AidKitCount--;
        _currentHealth = Mathf.Max(_currentHealth + healCount, _maxHealth);
        AidKitChanged?.Invoke(AidKitCount);
    }

    public void BuyItem(IShopItem item)
    {
        Money -= item.Price;

        MoneyChanged?.Invoke(Money);
        switch (item)
        {
            case Weapon weapon:
                _weapons.Add(weapon);
                Debug.Log($"Куплено оружие: {weapon.Label}");
                break;
            case AidKit kit:
                AidKitCount++;
                AidKitChanged?.Invoke(AidKitCount);
                Debug.Log($"Куплена аптечка. Всего аптечек: {AidKitCount}");
                break;
            case DoorUpgradeItem doorItem:
                Debug.Log("Двери ПРОКАЧАНЫ");
                break;
        }
    }

    public void SpendResources(int price, int fragmentsCount = 0)
    {
        Money -= price;
        MoneyChanged?.Invoke(Money);

        if (fragmentsCount > 0)
        {
            Fragments -= fragmentsCount;
            FragmentsChanged?.Invoke(Fragments);
        }
    }
    

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private bool IsDied()
    {
        return _currentHealth <= 0;
    }
}