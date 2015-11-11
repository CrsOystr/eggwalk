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
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped || pathPoints.Length == 0)
			return;

		transform.LookAt (pathPoints[pathIndex]);

		Vector3 newPos = Vector3.MoveTowards (transform.position, pathPoints [pathIndex].position, moveSpeed * Time.deltaTime);

		if (newPos == pathPoints [pathIndex].position) {
			pathIndex++;
			if(pathIndex > pathPoints.Length - 1) 
				pathIndex = 0;
		}
		transform.position = newPos;
	}
}
