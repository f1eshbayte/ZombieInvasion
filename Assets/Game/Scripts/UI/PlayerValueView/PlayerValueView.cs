using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class PlayerValueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    protected Player _player;

    protected abstract event UnityAction<int> OnValueChanged; // событие, на которое будем подписываться
    protected abstract int GetValue(Player player);     // откуда взять текущее значение

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void OnEnable()
    {
        OnValueChanged += UpdateValue;
        UpdateValue(GetValue(_player)); // сразу показать актуальное значение
    }

    private void OnDisable()
    {
        OnValueChanged -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        _text.text = value.ToString();
    }
}