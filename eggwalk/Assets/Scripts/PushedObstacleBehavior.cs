using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Triggerable))]

public class PushedObstacleBehavior : MonoBehaviour {

	public Vector3 applyForceAtPosition;
	public Vector3 forceDirection;
	public float forceMagnitude;
	
	private Rigidbody rigidBody;
	private Triggerable triggerable;
	private bool forceApplied;
	
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		triggerable = GetComponent<Triggerable> ();
		forceApplied = false;
	}
	
	void Update () { // because useGravity is initially set to false, we can't call FixedUpdate
		if (triggerable.isTriggered ()) {
			rigidBody.AddForceAtPosition(forceDirection.normalized * forceMagnitude, applyForceAtPosition, ForceMode.Impulse);
		}
	}
}
