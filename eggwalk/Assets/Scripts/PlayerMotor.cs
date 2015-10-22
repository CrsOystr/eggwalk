using UnityEngine;
using System.Collections;

/*
 * This moves the player (by adding forces and velocities to the player's rigidbody) based on signals from the PlayerController.
 * 
 * PlayerController = handles input
 * PlayerMotor = actually moves the player
 */

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	public float forwardSpeed; // the player's run speed (Z direction)
	public float strafeSpeed; // the player's strafe speed (X direction)
	public float jumpForce; // the force applied to the rigid body when the jump button is pressed

	private float xVel, zVel; // x = side to side, z = forward and back
	private Rigidbody rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// FixedUpdate e is used because we are doing things with physics.
	void FixedUpdate () {
		Vector3 currentVelocity = rigidBody.velocity;
		Vector3 newVelocity = new Vector3(
			xVel * Time.deltaTime,
			currentVelocity.y, // keep the rigidbody's y velocity
			zVel * Time.deltaTime);
		rigidBody.velocity = newVelocity;
	}

	public void addJumpForce() {
		// and jump force in the world's up direction
		rigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse); // ForceMode.Impulse creates more of a "pop" when the force is applied
	}

	public void setXZvelocity(float xInput, float zInput) {
		xVel = Mathf.Lerp(xVel, strafeSpeed * xInput, 0.1f); // use Mathf.Lerp to smooth values
		zVel = Mathf.Lerp(zVel, forwardSpeed, 0.5f);
	}
}
