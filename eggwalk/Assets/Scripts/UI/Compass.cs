using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    [SerializeField] private RectTransform gague;
    [SerializeField] private float pixelsBetweenNotch;
	
    public void moveGague(float pixels)
    {
        gague.anchoredPosition = new Vector2(gague.anchoredPosition.x + pixels, gague.anchoredPosition.y);
    }
}
