﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent (typeof (Rigidbody))]

public class BasicPickup : MonoBehaviour, Pickup {

    [SerializeField] private int id;
    [SerializeField] private string pickupName;
    [SerializeField] private int mass;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private GameObject pickupIndicatorObject;
	public float amplitude;
	public float rate;
    private bool canBob = true;

    void Start()
    {
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        this.gameObject.GetComponent<SphereCollider>().isTrigger = true;
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
    }

	void FixedUpdate() 
	{
        if (canBob)
        {
            //Vector3 BobbingVector = Vector3.up * amplitude * rate * Mathf.Sin(rate * Time.time) * Time.deltaTime;
            //this.transform.parent.Translate(BobbingVector, Space.Self);
        }
	}

	void OnTriggerEnter(Collider col)
	{

	}

    public int getId()
    {
        return this.id;
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

    public void onPickupAction()
    {
        Destroy(this.pickupIndicatorObject);
    }

    public void onDropAction()
    {
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.canBob = false;
    }
}
