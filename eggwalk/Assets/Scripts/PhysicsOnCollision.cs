using UnityEngine;
using System.Collections;

public class PhysicsOnCollision : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.GetComponent<Rigidbody> () != null && gameObject.GetComponent<Rigidbody>() == null) {

			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			rb.mass = 1;


		}
	}

}
