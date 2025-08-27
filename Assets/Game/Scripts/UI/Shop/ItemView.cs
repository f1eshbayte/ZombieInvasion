using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Image _icon;

    private IShopItem _item;

    public event UnityAction<IShopItem, ItemView> SellButtonClick; 
    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }
    
    public void Render(IShopItem item)
    {
        _item = item;
        
        _label.text = item.Label;
        _price.text = item.Price.ToString();
        _icon.sprite = item.Icon;
        
        TryLockItem();
    }

    public void DisableButton()
    {
        _sellButton.interactable = false;
    }

    private void TryLockItem()
    {
        if (_item is Weapon weapon && weapon.IsBuyed)
            DisableButton();
    }
    
    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_item, this);
    }
}