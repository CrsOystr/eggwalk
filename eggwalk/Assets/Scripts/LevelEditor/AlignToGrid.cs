using UnityEngine;
using UnityEditor;
using System.Collections;


[ExecuteInEditMode]
public class AlignToGrid : MonoBehaviour {
	
	public float gridSize;

	public bool alignToX, alignToY, alignToZ;
	void Update() {

		if(!EditorApplication.isPlaying) return;

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

		Debug.Log ("test");

	}

	private float roundToGridSize(float r) {
		return r - (r % gridSize);
	}

}
