using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class VehicleBehavior : MonoBehaviour {

	public enum DriveDirection {
		NORTH, SOUTH, EAST, WEST
	}

	public float driveSpeed; // target drive speed
	public float acceleration; // force applied to accelerate vehicle
	public float roadWidth; // (minimum) width of the road this vehicle will drive along
	public bool driveOnRight; // does this car drive on the left side of the road or the right side?
	public Transform target; // where is the next point this car is driving too?
	public float turnSpeed;
	public DriveDirection direction;

	private Rigidbody rb;
	private Ray frontRay, backRay, leftRay, rightRay;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		frontRay = new Ray (transform.position, transform.forward);
		backRay = new Ray (transform.position, -transform.forward);
		leftRay = new Ray (transform.position, -transform.right);
		rightRay = new Ray (transform.position, transform.right);
	}
	
	// Update is called once per frame
	void Update () {

		// turn toward target
		turnTowardTarget ();

		// accelerate to desired speed
		if (rb.velocity.magnitude < driveSpeed) {
			rb.AddForce(transform.forward * acceleration, ForceMode.Force);
		}

	}
	
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Intersection") {
			pickNextTarget();
			Debug.Log (col.gameObject.tag);
		}
	}

	private void turnTowardTarget () {
		Quaternion targetRotation = Quaternion.LookRotation (target.position - transform.position);

		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, 1);
	}

	private void pickNextTarget() {
		bool success = false;
		int choose;
		DriveDirection nextDir;
		IntersectionBehavior ib = target.GetComponentInParent<IntersectionBehavior> ();

		// randomly pick the next direction
		while (!success) {
			choose = Random.Range(0, 4);

			switch(choose) {
			case 0: // pick north
				if(ib.toNorth != null && direction != DriveDirection.SOUTH) {
					Debug.Log(ib.name);
					direction = DriveDirection.NORTH;
					target = ib.toNorth.transform.Find("SouthEast");
					success = true;
					return;
				}
				break;
			case 1: // pick south
				if(ib.toSouth != null && direction != DriveDirection.NORTH) {
					Debug.Log(ib.name);
					direction = DriveDirection.SOUTH;
					target = ib.toSouth.transform.Find("NorthWest");
					success = true;
					return;
				}
				break;
			case 2: // pick east
				if(ib.toEast != null && direction != DriveDirection.WEST) {
					Debug.Log(ib.name);
					direction = DriveDirection.EAST;
					target = ib.toEast.transform.Find("SouthWest");
					success = true;
					return;
				}
				break;
			case 3: // pick west
				if(ib.toWest != null && direction != DriveDirection.EAST) {
					Debug.Log(ib.name);
					direction = DriveDirection.WEST;
					target = ib.toWest.transform.Find("NorthEast");
					success = true;
					return;
				}
				break;
			}
		}

	}


}
