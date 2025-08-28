using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameOverScreen;

    private Player _player;
    private Menu _menu;
    // private DoorUpgradeItem _doorUpgradeItem;
    
    [Inject]
    public void Construct(Player player, Menu menu/*, DoorUpgradeItem doorUpgradeItem*/)
    {
        _player = player;
        _menu = menu;
        // _doorUpgradeItem = doorUpgradeItem;
    }

    private void Start()
    {
        _menu.OpenPanelAndStopTime(_startScreen);
    }

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
    }

    public void OnRestartButtonCLick(GameObject panel)
    {
        _menu.ClosePanel(panel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // _doorUpgradeItem.ResetUpgradeIndex();
    }

    public void OnPlayButtonClick()
    {
        _menu.ClosePanel(_startScreen);
    }

    private void OnGameOver()
    {
        _menu.OpenPanelAndStopTime(_gameOverPanel);
        _menu.OpenPanel(_gameOverScreen);
    }
}
