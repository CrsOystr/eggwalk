using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class TurningVolume : MonoBehaviour {

	public BoxCollider boxCollider;
	public bool canTurnRight;
	public bool canTurnLeft;
    public bool isPlayerTurning;

	// Use this for initialization
	void Start () {
		boxCollider = this.gameObject.GetComponent<BoxCollider> ();
	}

    public bool IsPlayerTurning
    {
        get { return isPlayerTurning; }
        set { isPlayerTurning = value; }
    }
}