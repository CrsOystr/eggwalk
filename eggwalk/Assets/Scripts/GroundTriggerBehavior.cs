using UnityEngine;
using System.Collections;

/*
 * This class 
 */ 

public class GroundTriggerBehavior : MonoBehaviour {
	
	public GameObject user; // the gameobject "using" this component
	
	private bool onGround = false;
	private ArrayList standingOn = new ArrayList(); // this needs to be an array, because the ground trigger can intersect more than one object
	
	void OnTriggerEnter(Collider col)
	{
		standingOn.Add(col.gameObject.transform);
		user.transform.parent = col.transform;
		onGround = true;
	}
	
	void OnTriggerExit(Collider col)
	{
		// remove object
		standingOn.Remove(col.gameObject.transform);
		
		// check for whether or not the user is standing on any other objects
		if (standingOn.Count == 0)
		{
			onGround = false; // only off the ground when there are no objects in the standingOn array
			user.transform.parent = null;
		} else
		{
			user.transform.parent = standingOn[standingOn.Count - 1] as Transform;
		}
	}
	
	public bool isOnGround()
	{
		return onGround;
	}
}
