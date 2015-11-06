using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToFloor : MonoBehaviour {

	public float threshold;

	public bool active; // turn on/off snapping behavior

	void Update () {
#if UNITY_EDITOR

		RaycastHit hit;

		if(Physics.Raycast(transform.position, -Vector3.up, out hit) && active) {
			if(hit.distance <= threshold) {
				Vector3 newPos = new Vector3 (
					transform.position.x,
					transform.position.y - hit.distance,
					transform.position.z);

				transform.position = newPos;
			}
		}

#endif
	}
}
