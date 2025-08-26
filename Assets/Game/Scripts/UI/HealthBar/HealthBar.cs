using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _healthBar;

    private Player _player;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _healthBar.value = 1;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth, int maxHeath)
    {
        _healthBar.value = (float)currentHealth / maxHeath;
    }
}
