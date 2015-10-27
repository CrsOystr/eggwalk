using UnityEngine;
using System.Collections;

public class RoofObstacle : MonoBehaviour, Obstacle {
    public bool IsActivated;
    public float FallingSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsActivated) {
            activate();
        }
	}

    public void activate()
    {
        this.IsActivated = true;
    }
}
