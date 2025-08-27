using UnityEngine.Events;

public class MoneyBalance : PlayerValueView
{
    protected override event UnityAction<int> OnValueChanged
    {
        add => _player.MoneyChanged += value;
        remove => _player.MoneyChanged -= value;
    }

    protected override int GetValue(Player player) => player.Money;
}