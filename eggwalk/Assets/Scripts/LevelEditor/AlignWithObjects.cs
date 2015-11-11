using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class AlignWithObjects : MonoBehaviour {

	public float snapThreshold;
	public bool alignToX, alignToY, alignToZ;
	public bool active;

	private GameObject[] objects;
	private bool snapped;
	private Transform snappedTransform;

	// only run when the object is being moved
	void Update () {
#if UNITY_EDITOR

		snapped = false;

		if(!active) return;

		if (gameObject == Selection.activeObject) {
			objects = FindObjectsOfType<GameObject>();

			// in the case that more than one object is within the snapping threshold, use these variables to make sure it always snaps to the closest
			float closestX = snapThreshold;
			float closestY = snapThreshold;
			float closestZ = snapThreshold;
			
			for(int i = 0; i < objects.Length; i++) {

				// shoddy work-around, but this makes sure it will only snap to imported FBX files, and not their individual component meshes
				if(objects[i].GetComponent<Animator>() == null) continue;
				
				// check to make sure we aren't comparing the gameobject to itself
				if(gameObject == objects[i]) continue;

				// only check against active objects
				if(!objects[i].activeInHierarchy) continue;

				if(alignToX) {
					float distance = Mathf.Abs(transform.position.x - objects[i].transform.position.x);
					if(distance < closestX) {
						closestX = distance;
						Vector3 newPos = new Vector3 (
							objects[i].transform.position.x,
							transform.position.y,
							transform.position.z);
						transform.position = newPos;
						
						snapped = true;
						snappedTransform = objects[i].transform;
					}
				}

				if(alignToY) {
					float distance = Mathf.Abs(transform.position.y - objects[i].transform.position.y);
					if(distance < closestY) {
						closestY = distance;
						Vector3 newPos = new Vector3 (
							transform.position.x,
							objects[i].transform.position.y,
							transform.position.z);
						transform.position = newPos;
						
						snapped = true;
						snappedTransform = objects[i].transform;
					}
				}

				if(alignToZ) {
					float distance = Mathf.Abs(transform.position.z - objects[i].transform.position.z);
					if(distance < closestZ) {
						closestZ = distance;
						Vector3 newPos = new Vector3 (
							transform.position.x,
							transform.position.y,
							objects[i].transform.position.z);
						transform.position = newPos;

						snapped = true;
						snappedTransform = objects[i].transform;
					}
				}

			}
			
			if(snapped) Debug.DrawLine(transform.position, snappedTransform.transform.position, Color.magenta, 2.0f, false);
		}
#endif
	}
}
