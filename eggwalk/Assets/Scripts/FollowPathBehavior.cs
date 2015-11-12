using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class FollowPathBehavior : MonoBehaviour {

	public float moveSpeed;

	public Transform[] pathPoints; // the array of points the object move towards
	// !!!!! these points MUST NOT be children of this object, they must exist independent of this object !!!!!!

	public bool stopped;

	private int pathIndex;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		pathIndex = 0;
		rigidbody = GetComponent<Rigidbody> ();

		removeY (); // set all y values in pathPoints to be equal to this object's y (paths are only 2D)
		transform.LookAt (pathPoints[pathIndex]);
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped || pathPoints.Length == 0)
			return;


		Vector3 newPos = Vector3.MoveTowards (transform.position, pathPoints [pathIndex].position, moveSpeed * Time.deltaTime);

		if (newPos == pathPoints [pathIndex].position) {
			pathIndex++;
			if(pathIndex > pathPoints.Length - 1) 
				pathIndex = 0;

			transform.LookAt (pathPoints[pathIndex]);
		}

		transform.position = newPos;
	}

	private void removeY() {
		for (int i = 0; i < pathPoints.Length; i++) {
			Vector3 newPos = new Vector3 (
				pathPoints[i].position.x,
				transform.position.y,
				pathPoints[i].position.z);

			pathPoints[i].position = newPos;
		}
	}
}
