using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]

public class Pickup : MonoBehaviour {

	public float amplitude;
	public float rate;

	void FixedUpdate() 
	{
		Vector3 BobbingVector = Vector3.up * amplitude * rate * Mathf.Sin (rate * Time.time) * Time.deltaTime;
		this.transform.Translate (BobbingVector);
	}

	void OnTriggerEnter(Collider col)
	{
        Destroy(this.gameObject);
	}
}
