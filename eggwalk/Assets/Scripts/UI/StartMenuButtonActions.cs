using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartMenuButtonActions : MonoBehaviour {

    public WidgetSwitcher MenuSwitcher;

    public void GoToMenu(GameObject menu)
    {
        MenuSwitcher.SetCurrentWidget(menu);
    }

    public void SelectLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
