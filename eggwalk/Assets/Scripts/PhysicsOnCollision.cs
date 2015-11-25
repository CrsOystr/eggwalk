using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsOnCollision : MonoBehaviour {

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
	}

	void OnCollisionEnter(Collision col) {

		Rigidbody colRB = col.gameObject.GetComponent<Rigidbody> ();

		if (colRB != null)
			rb.isKinematic = false;

	}

}
