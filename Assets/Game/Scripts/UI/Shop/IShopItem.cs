using UnityEngine;

public interface IShopItem
{
    public string Label { get; }
    public int Price { get; }
    public Sprite Icon { get; }
}