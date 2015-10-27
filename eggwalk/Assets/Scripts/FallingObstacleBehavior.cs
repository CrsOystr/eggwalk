using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Triggerable))]

public class FallingObstacleBehavior : MonoBehaviour {

	private Rigidbody rigidBody;
	private Triggerable triggerable;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		triggerable = GetComponent<Triggerable> ();

		rigidBody.useGravity = false;
	}

	void Update () { // because useGravity is initially set to false, we can't call FixedUpdate
		if (triggerable.isTriggered ())
			rigidBody.useGravity = true;
	}
}
