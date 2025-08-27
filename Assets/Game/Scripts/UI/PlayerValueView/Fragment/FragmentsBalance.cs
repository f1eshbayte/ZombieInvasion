using UnityEngine.Events;

public class FragmentsBalance : PlayerValueView
{
    protected override event UnityAction<int> OnValueChanged
    {
        add => _player.FragmentsChanged += value;
        remove => _player.FragmentsChanged -= value;
    }

    protected override int GetValue(Player player) => player.Fragments;
}
