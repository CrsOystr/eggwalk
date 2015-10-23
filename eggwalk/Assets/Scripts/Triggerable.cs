using UnityEngine;
using System.Collections;

/*
 * A generic script that allows an object to check if every object in an array of GameObjects with SwitchBehavior scripts have been activated,
 * thus triggering some event to take place.
 *
 * Setting the 'switches' array to size 0 in the inspector will cause the trigger to instantly activate.
 */

public class Triggerable : MonoBehaviour {
	
	public GameObject[] switches;
	
	private bool triggered;
	private SwitchBehavior[] sbArray; // create an array of SwitchBehaviors to avoid calling expensive GetComponent() each frame
	
	// Use this for initialization
	void Start () {
		triggered = false;
		sbArray = createSBArray (switches);
	}
	
	// Update is called once per frame
	void Update () {
		int numberOn = 0; // variable to count how many triggers are on
		for (int s = 0; s < switches.Length; s++)
		{
			if (sbArray[s].isSwitchOn()) numberOn++;
		}
		
		if (numberOn == switches.Length) triggered = true;
		else triggered = false;
	}

	private SwitchBehavior[] createSBArray(GameObject[] s) {
		sbArray = new SwitchBehavior[s.Length];

		for (int i = 0; i < s.Length; i++) {
			sbArray[i] = s[i].GetComponent<SwitchBehavior>();
		}

		return sbArray;
	}

	// proof of concept: adding switches dynamically
	// can write "removeSwitch(GameObject s)" function if needed
	public void addSwitch(GameObject s) {
		// create temporary array that is one object larger than 'switches'
		GameObject[] newArray = new GameObject[switches.Length + 1];

		// copy over all the objects from 'switches' to 'newArray'
		for (int i = 0; i < switches.Length; i++) {
			newArray[i] = switches[i];
		}

		// add in the new object to last index
		newArray [newArray.Length - 1] = s;

		// copy over 'newArray' back to 'switches'
		switches = newArray;
		// rebuild 'sbArray'
		sbArray = createSBArray(switches);
	}
	
	public bool isTriggered()
	{
		return triggered;
	}
	
	public void setTriggered(bool t)
	{
		triggered = t;
	}
}
