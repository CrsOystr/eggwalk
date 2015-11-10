using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class TurningVolume : MonoBehaviour {

	public BoxCollider boxCollider;
	public bool canTurnRight;
	public bool canTurnLeft;

	// Use this for initialization
	void Start () {
		boxCollider = this.gameObject.GetComponent<BoxCollider> ();
	}
}
