using TMPro;
using UnityEngine;
using Zenject;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;

    private Player _player;
    
    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _money.text = _player.Money.ToString();
    }
}
