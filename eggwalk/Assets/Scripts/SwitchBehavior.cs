using UnityEngine;
using System.Collections;

/*
 * This is a generic component that can be attached to any object, such as a light sensor, button, etc., that gives it switch functionality.
 */

public class SwitchBehavior : MonoBehaviour {
	
	public bool isOn;
	
	public void setOn(bool o)
	{
		isOn = o;
	}
	
	public bool isSwitchOn()
	{
		return isOn;
	}
}
