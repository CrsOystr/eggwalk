using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (SphereCollider))]

public class ExplodePickup : MonoBehaviour, Pickup {

    [SerializeField] private string pickupName;
    [SerializeField] private int mass;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private List<Rigidbody> fragmentRigidBodies;

	// Use this for initialization
	void Start () {
        /*for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].useGravity = true;
            fragmentRigidBodies[i].isKinematic = true;
        }*/
    }

    public string getName()
    {
        return this.pickupName;
    }

    public int getMass()
    {
        return this.mass;
    }

    public Transform getCenterOfMass()
    {
        return this.centerOfMass;
    }

    public GameObject getTargetItem()
    {
        return this.gameObject;
    }

    public void pickupAction()
    {
        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].isKinematic = false;
        }
    }
}
