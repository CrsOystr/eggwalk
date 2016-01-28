﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class VehicleBehavior : MonoBehaviour {

	public enum DriveDirection {
		NORTH, SOUTH, EAST, WEST
	}

	public bool debug;
	public float driveSpeed; // target drive speed
	public float acceleration; // force applied to accelerate vehicle
	public float roadWidth; // (minimum) width of the road this vehicle will drive along
	public bool driveOnRight; // does this car drive on the left side of the road or the right side?
	public float targetDistThreshold; // how close can you get to the target before choosing another?
	//public float turnSpeed;
	public DriveDirection direction;
	
	private GameObject target; // where is the next point this car is driving too?
	public GameObject Target {
		get { return target;}
		set { target = value;}
	}
	private Rigidbody rb;
	private Ray frontRay, backRay, leftRay, rightRay;
	private bool targetSelected;
	private float startY;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		frontRay = new Ray (transform.position, transform.forward);
		backRay = new Ray (transform.position, -transform.forward);
		leftRay = new Ray (transform.position, -transform.right);
		rightRay = new Ray (transform.position, transform.right);

		targetSelected = true;
		
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// turn toward target
		turnTowardTarget ();

		// accelerate to desired speed
		
		rb.AddForce(transform.forward * acceleration, ForceMode.Force);
		if (rb.velocity.magnitude > driveSpeed) {
			Vector3 newVel = rb.velocity.normalized * driveSpeed;
			rb.velocity = newVel;
		}

		float distToTarget = Vector3.Distance (transform.position, target.transform.position);

		if (distToTarget < targetDistThreshold) {
			pickNextTarget ();
		}

		if (debug) {
			Debug.DrawLine(transform.position, target.transform.position);
		}
		
		if(transform.position.y > startY) {
			rb.AddForce(Vector3.up * -100f);
		}

	}

	private void turnTowardTarget () {
		Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);

		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 0.1f);
	}

	private void pickNextTarget() {
		bool success = false;
		int choose;
		IntersectionBehavior ib = target.GetComponentInParent<IntersectionBehavior> ();

		// randomly pick the next direction
		while (!success) {
			choose = Random.Range(0, 4);

			switch(choose) {
			case 0: // pick north
				if(ib.toNorth != null && direction != DriveDirection.SOUTH) {
					direction = DriveDirection.NORTH;
					target = ib.toNorth.transform.Find("SouthEast").gameObject;
					success = true;
					return;
				}
				break;
			case 1: // pick south
				if(ib.toSouth != null && direction != DriveDirection.NORTH) {
					direction = DriveDirection.SOUTH;
					target = ib.toSouth.transform.Find("NorthWest").gameObject;
					success = true;
					return;
				}
				break;
			case 2: // pick east
				if(ib.toEast != null && direction != DriveDirection.WEST) {
					direction = DriveDirection.EAST;
					target = ib.toEast.transform.Find("SouthWest").gameObject;
					success = true;
					return;
				}
				break;
			case 3: // pick west
				if(ib.toWest != null && direction != DriveDirection.EAST) {
					direction = DriveDirection.WEST;
					target = ib.toWest.transform.Find("NorthEast").gameObject;
					success = true;
					return;
				}
				break;
			}
		}

		Vector3 adjTarget = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
		target.transform.position = adjTarget;

	}


}
