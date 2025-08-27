using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class Task : MonoBehaviour
{
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private int _fragments;
    [SerializeField] private Sprite _doneSpite;
    [SerializeField] private Button _performButton;
    [SerializeField] private TMP_Text _textDescription;
    [SerializeField] private TMP_Text _textPrice;

    private Player _player;
    public bool IsDone { get; private set; } = false;

    public event UnityAction<Task> OnTaskCompleted;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _textDescription.text = _description;
        _textPrice.text = GetPriceText();
    }
    
    private string GetPriceText()
    {
        string result = "";

        if (_price > 0)
            result += $"{_price} монет";

        if (_fragments > 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " + ";
            result += $"{_fragments} фрагментов";
        }

        return result;
    }


    private void OnEnable()
    {
        _performButton.onClick.AddListener(TryCompleteTack);
    }

    private void OnDisable()
    {
        _performButton.onClick.RemoveListener(TryCompleteTack);
    }

    private void TryCompleteTack()
    {
        if (IsDone)
            return;

        if (!HasEnoughResources())
            return;
        
        _player.SpendResources(_price, _fragments);

        CompleteTask();
    }

    private bool HasEnoughResources()
    {
        if (_price > 0 && _player.Money < _price)
            return false;

        if (_fragments > 0 && _player.Fragments < _fragments)
            return false;

        return true;
    }
    private void CompleteTask()
    {
        IsDone = true;
        _textPrice.enabled = false;
        _performButton.image.sprite = _doneSpite;
        OnTaskCompleted?.Invoke(this);
    }
}