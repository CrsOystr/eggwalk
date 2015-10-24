using UnityEngine;
using System.Collections;

/*
 * This moves the player (by adding forces and velocities to the player's rigidbody) based on signals from the PlayerController.
 * 
 */

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	public float forwardSpeed; // the player's run speed (Z direction)
	public float strafeSpeed; // the player's strafe speed (X direction)

	private float xVel, zVel; // x = side to side, z = forward and back

	public float cameraBobRate;
	public float cameraBobHeight;

	//handles the lives for player
	public int playerLives = 5;
	private int currentLives;

	//Important objects used by player
	private Rigidbody rigidBody;
	public GameObject playerCamera;

	//Backlog stuff
	public float jumpForce; // the force applied to the rigid body when the jump button is pressed

	//Initializes our player
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	//use this for stuff like inputs
	void Update () {
		float horzInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");
		
		setXZvelocity (horzInput, vertInput);

	}

	// FixedUpdate e is used because we are doing things with physics.
	void FixedUpdate () {
		updateVelocity ();
		updateCamera ();
	}
	private void updateCamera (){
		if (Time.time % cameraBobRate < (cameraBobRate / 2)) 
			playerCamera.transform.position += new Vector3 (0, cameraBobHeight*Time.deltaTime, 0);
		else
			playerCamera.transform.position += new Vector3 (0, -cameraBobHeight*Time.deltaTime, 0);
	}

	private void updateVelocity(){
		Vector3 currentVelocity = rigidBody.velocity;
		Vector3 newVelocity = new Vector3(
			xVel * Time.deltaTime,
			currentVelocity.y, // keep the rigidbody's y velocity
			zVel * Time.deltaTime);
		rigidBody.velocity = newVelocity;
	}

	public void setXZvelocity(float xInput, float zInput) {
		xVel = Mathf.Lerp(xVel, strafeSpeed * xInput, 0.1f); // use Mathf.Lerp to smooth values
		zVel = Mathf.Lerp(zVel, forwardSpeed, 0.5f);
	}


	public void addJumpForce() {
		// and jump force in the world's up direction
		rigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse); // ForceMode.Impulse creates more of a "pop" when the force is applied
	}
}
