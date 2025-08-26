using TMPro;
using UnityEngine;
using Zenject;

public class AidKitCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _aidKit;

    private Player _player;
    
    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    private void OnEnable()
    {
        _player.AidKitChanged += OnAidKitChanged;
    }

    private void OnDisable()
    {
        _player.AidKitChanged -= OnAidKitChanged;
    }

    private void OnAidKitChanged(int aidKitCount)
    {
        _aidKit.text = _player.AidKitCount.ToString();
    }
}
