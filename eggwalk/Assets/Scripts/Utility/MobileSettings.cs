using UnityEngine;
using System.Collections;

public class MobileSettings : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;

		Screen.orientation = ScreenOrientation.AutoRotation;
	}
}
