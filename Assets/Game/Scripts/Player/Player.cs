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
    public int Money { get; private set; }
    public int Fragments { get; private set; }
    public float Speed => _speed;
    public Weapon CurrentWeapon => _currentWeapon;
    
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int> AidKitChanged;
    public event UnityAction<int> FragmentsChanged;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> DamageTaken;
    public event UnityAction GameOver;

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

    public void AddFragments(int count)
    {
        Fragments += count;
        FragmentsChanged?.Invoke(Fragments);
    }   

    public void Heal(int healCount)
    {
        if(AidKitCount <= 0)
            return;
        
        Debug.Log("Хилл из Player");
        
        AidKitCount--;
        _currentHealth = Mathf.Min(_currentHealth + healCount, _maxHealth);
        AidKitChanged?.Invoke(AidKitCount);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
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
            case AidKit:
                AidKitCount++;
                AidKitChanged?.Invoke(AidKitCount);
                Debug.Log($"Куплена аптечка. Всего аптечек: {AidKitCount}");
                break;
            case DoorUpgradeItem:
                Debug.Log("Двери ПРОКАЧАНЫ");
                break;
            case DoorRepairItem:
                Debug.Log("Двери ПОЧИНЕНЫЫЫЫЫ");
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
        GameOver?.Invoke();
    }

    private bool IsDied()
    {
        return _currentHealth <= 0;
    }
}