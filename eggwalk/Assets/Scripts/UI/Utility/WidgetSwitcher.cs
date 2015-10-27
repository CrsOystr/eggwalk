using UnityEngine;
using System.Collections.Generic;

public class WidgetSwitcher : MonoBehaviour {

    public int CurrentWidgetIndex;
    public List<GameObject> WidgetList;

    void Start()
    {
        for(int i = 0; i < WidgetList.Count; i++)
        {
            WidgetList[i].SetActive(false);
        }

        WidgetList[this.CurrentWidgetIndex].SetActive(true);
    }

    public void SetCurrentWidgetIndex(int index)
    {
        WidgetList[this.CurrentWidgetIndex].SetActive(false);
        WidgetList[index].SetActive(true);
        this.CurrentWidgetIndex = index;
    }

    public void SetCurrentWidget(GameObject widget)
    {
        WidgetList[this.CurrentWidgetIndex].SetActive(false);

        for (int i = 0; i < WidgetList.Count; i++)
        {
            if (WidgetList[i] == widget)
            {
                WidgetList[i].SetActive(true);
                this.CurrentWidgetIndex = i;
            }
        }
    }
}
