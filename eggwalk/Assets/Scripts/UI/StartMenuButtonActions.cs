using UnityEngine;
using System.Collections.Generic;

public class StartMenuButtonActions : MonoBehaviour {

    public WidgetSwitcher MenuSwitcher;

    public void GoToMenu(GameObject menu)
    {
        MenuSwitcher.SetCurrentWidget(menu);
    }

    public void SelectLevel(string level)
    {
        Application.LoadLevel(level);
    }
}
