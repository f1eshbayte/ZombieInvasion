using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public static event UnityAction<bool> OnMenuStateChanged; // true = открыт, false = закрыт

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void OpenPanelAndStopTime(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);

        Time.timeScale = 1;
    }

    public void InvokeStateOpenMenu()
    {
        OnMenuStateChanged?.Invoke(true);
    }

    public void InvokeStateCloseMenu()
    {
        OnMenuStateChanged?.Invoke(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}