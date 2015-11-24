using UnityEngine;
using System.Collections;

public class LightpostBehavior : MonoBehaviour {

	public Light spotlight;
	public float maxDelay; // maximum time to delay turning on/off light
	public float lightIntensity;

	private SunBehavior sb;
	private float delay; // random delay to turn on/off light

	// Use this for initialization
	void Start () {
		sb = GameObject.FindGameObjectWithTag ("Sun").GetComponent<SunBehavior> ();
		spotlight.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float timeFraction = sb.getTimeFraction (); // 0.5 == noon

		// turn lights on at night, turn them off during the day
		if (timeFraction < 0.25f || timeFraction > 0.75f) { // it's night
			if(delay <= 0f && spotlight.intensity == 0) { // delay has NOT been set and light is OFF
				delay = Random.value * maxDelay; // set delay
			} else if(delay > 0f) { // delay HAS been set
				delay -= Time.deltaTime;
				if(delay <= 0f) spotlight.intensity = lightIntensity; // turn on light
			}
		} else { // it's day
			if(delay <= 0f && spotlight.intensity == lightIntensity) { // delay has NOT been set and light is ON
				delay = Random.value * maxDelay; // set delay
			} else if(delay > 0f) { // delay HAS been set
				delay -= Time.deltaTime;
				if(delay <= 0f) spotlight.intensity = 0; // turn off light
			}
		}
	}
}
