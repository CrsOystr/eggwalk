using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class VehicleBehavior : MonoBehaviour {

	public float driveSpeed; // target drive speed
	public float acceleration; // force applied to accelerate vehicle

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		// accelerate to desired speed
		if (rb.velocity.magnitude < driveSpeed) {
			rb.AddForce(transform.forward * acceleration, ForceMode.Force);
		}

	}
}
