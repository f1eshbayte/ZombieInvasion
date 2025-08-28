using UnityEngine;
using Zenject;

public class AidKit : MonoBehaviour, IShopItem
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _healCount;
    
    private Player _player;
        
    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public int HealCount => _healCount;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    // потом сделать по нажатию на кнопку хилл(наверное сигнал бас)

    // public void Heal()
    // {
    //     _player.Heal(_healCount);
    // }
}
