using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]

public class SunBehavior : MonoBehaviour {

	public Vector3 noonRotation;

	public float hours, minutes; // enter time (24-hour time, i.e. 13:00 instead of 1:00PM)
	private Quaternion noonRotationQ; // x = 50, y = 330, z = 0
	private Light sun;

	// Use this for initialization
	void Start () {
		noonRotationQ = Quaternion.Euler (noonRotation);
		sun = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float fraction = getTimeFraction ();

		sun.transform.rotation = calculateSunRotation (fraction);

		if (fraction < 0.25 || fraction > 0.75) {
			sun.intensity = Mathf.Lerp (sun.intensity, 0f, 0.1f);
		} else {
			sun.intensity = Mathf.Lerp (sun.intensity, 1f, 0.1f);
		}
	}

	private float getTimeFraction() { // 0.5 = noon
		float adjMinutes = minutes/60;
		float adjHour = (hours + adjMinutes) % 24;
		
		return adjHour / 24f;
	}

	private Quaternion calculateSunRotation (float timeFraction) {
		Quaternion sunRotation = noonRotationQ * Quaternion.Euler (Vector3.up * ((timeFraction * 360) - 180));

		return sunRotation;
	}
}
