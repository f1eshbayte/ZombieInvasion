using UnityEngine.Events;

public class AidKitBalance : PlayerValueView
{
    protected override event UnityAction<int> OnValueChanged
    {
        add => _player.AidKitChanged += value;
        remove => _player.AidKitChanged -= value;
    }

    protected override int GetValue(Player player) => player.AidKitCount;
}