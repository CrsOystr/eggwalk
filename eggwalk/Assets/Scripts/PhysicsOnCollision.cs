using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsOnCollision : MonoBehaviour {

	[SerializeField] private float _secondsToReset;

	private Vector3 _initPosition;
	private Quaternion _initRotation;
	private float _resetCounter;
	private Rigidbody _rb;

	void Start () {
		_rb = GetComponent<Rigidbody> ();
		_rb.isKinematic = true;
		
		_initPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		_initRotation = Quaternion.Euler(transform.rotation.eulerAngles);
	}
	
	void Update() {
		if(_secondsToReset > 0) {
		
			_resetCounter += Time.deltaTime;
			if(_resetCounter >= _secondsToReset) {
			
				if(!isInsideCameraFrustrum()) {
					transform.position = _initPosition;
					transform.rotation = _initRotation;
                    _resetCounter = 0;
				} else {
					_resetCounter = 0;
					//Debug.Log ("it was on screen...");
				}
			
			}
		
		}
	}
	
	private bool isInsideCameraFrustrum() {
	
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds);
		
	}

	void OnCollisionEnter(Collision col) {

		Rigidbody colRB = col.gameObject.GetComponent<Rigidbody> ();

		if (colRB != null)
			_rb.isKinematic = false;

	}

}
