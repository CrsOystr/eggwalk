﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (SphereCollider))]

public class ExplodePickup : MonoBehaviour, Pickup {

    [SerializeField] private string pickupName;
    [SerializeField] private int mass;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private List<GameObject> fragmentRigidBodies;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < fragmentRigidBodies.Count; i++)
        {
            fragmentRigidBodies[i].GetComponent<BoxCollider>().enabled = false;
            //fragmentRigidBodies[i].useGravity = true;
            //fragmentRigidBodies[i].isKinematic = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 BobbingVector = Vector3.up * 0.01f * 10 * Mathf.Sin(10 * Time.time) * Time.deltaTime;
        this.transform.Translate(BobbingVector);
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
            fragmentRigidBodies[i].GetComponent<BoxCollider>().enabled = true;
            fragmentRigidBodies[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}