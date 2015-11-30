using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    [SerializeField] private Transform worldDestination;

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
	    if (worldDestination != null)
        {
            // Lock Rotation to just Up vector
            //Vector3 target = new Vector3(worldDestination.position.x, this.transform.position.y, worldDestination.position.z);

            this.transform.LookAt(worldDestination);
        }
	}

    public Transform WorldDestination
    {
        get { return this.worldDestination; }
        set { this.worldDestination = value; }
    }

    public void setActive(bool val)
    {
        this.GetComponent<MeshRenderer>().enabled = val;
    }
}
