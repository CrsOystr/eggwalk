using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Triggerable))]

public class SlidingObstacleBehavior : MonoBehaviour {

	public float moveLerp; // how fast the object slides to the target position;
	public Vector3 targetOffset; // the position (relative the this object's initial position!) the object should slide to

	private GameObject player;
	private Triggerable triggerable;
	private Rigidbody rigidBody;
	private bool targetSet;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		triggerable = GetComponent<Triggerable> ();
		rigidBody = GetComponent<Rigidbody> ();
		targetSet = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (triggerable.isTriggered () && !targetSet) {
			target = transform.position + targetOffset;
			targetSet = true;
		}

		if (targetSet) {
			Vector3 newPos = Vector3.Lerp(transform.position, target, moveLerp);
			rigidBody.MovePosition(newPos);
		}
	}
}
