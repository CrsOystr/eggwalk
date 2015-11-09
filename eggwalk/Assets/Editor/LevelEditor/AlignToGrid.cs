using UnityEngine;
using UnityEditor;
using System.Collections;


[ExecuteInEditMode]
public class AlignToGrid : MonoBehaviour {
	
	public float gridSize;

	public bool alignToX, alignToY, alignToZ;
	void Update() {

// this will run in both edit and play mode, but shouldn't run in a build of the game
#if UNITY_EDITOR

		float newX, newY, newZ;
		
		if (alignToX)
			newX = roundToGridSize (transform.position.x);
		else
			newX = transform.position.x;
		
		if (alignToY)
			newY = roundToGridSize (transform.position.y);
		else
			newY = transform.position.y;
		
		if (alignToZ)
			newZ = roundToGridSize (transform.position.z);
		else
			newZ = transform.position.z;

		Vector3 newPos = new Vector3 (newX, newY, newZ);

		transform.position = newPos;

#endif

	}

	private float roundToGridSize(float r) {
		return r - (r % gridSize);
	}

}
