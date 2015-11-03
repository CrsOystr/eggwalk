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
	public float rotationDueToGravity; // how much 'gravity' pulls the player over (not actually Rigidbody gravity)
	public float dropAtRotation; // how far the player can tip over before dropping the item
	public float tipsiness; // how easily the player tips over using input
	public float balanceOverpower; // scales the balance input so that tilting opposite directions on the left and right sticks still allows the player to correct their balance. 
									// Otherwise the player would not rotate.

	private float xVel, zVel; // x = side to side, z = forward and back
	private float horzInput, vertInput, balanceInput;

	// camera bob values
	public float cameraBobRate;
	public float cameraBobAmplitude;
	private float cameraStartHeight;

	//player life
	private bool isAlive;
	public int playerLives = 5;
	private int currentLives;
	private float lifeLossCooldown; // how quickly the player can lose life, so they don't lose all their lives at once
	private float lifeLossCooldownCounter;

	//Important objects used by player
	private Rigidbody rigidBody;
	public GameObject playerCamera;

	//Backlog stuff
	public float jumpForce; // the force applied to the rigid body when the jump button is pressed

	//Initializes our player
	void Start () {
		isAlive = true;
		rigidBody = GetComponent<Rigidbody> ();
		cameraStartHeight = playerCamera.transform.localPosition.y;

		currentLives = playerLives;
		lifeLossCooldown = 1; // one second between lives lost
		lifeLossCooldownCounter = 0f; // start at zero, because you can lose life immediately at the start of the game
	}

	//use this for stuff like inputs
	void Update () {
		horzInput = Input.GetAxis ("Horizontal");
		vertInput = Input.GetAxis ("Vertical");
		balanceInput = Input.GetAxis ("Balance");

		if (Mathf.Abs (balanceInput) < 0.1f)
			balanceInput = 0f;
		balanceInput *= balanceOverpower;

		Debug.Log (balanceInput);
		
		setXZvelocity (horzInput, vertInput);

		if (lifeLossCooldownCounter > 0) {
			lifeLossCooldownCounter -= Time.deltaTime;
			if(lifeLossCooldownCounter < 0) lifeLossCooldownCounter = 0;
		}
	}

	void FixedUpdate () {
		updateRotation ();
		updateVelocity ();
		updateCamera ();
	}
	
	private void updateCamera (){
		float newHeight = cameraStartHeight + (cameraBobAmplitude*Mathf.Sin(cameraBobRate*Time.time));
		playerCamera.transform.localPosition = new Vector3(
			playerCamera.transform.localPosition.x,
			newHeight,
			playerCamera.transform.localPosition.z);
	}

	private void updateVelocity(){
		Vector3 currentVelocity = rigidBody.velocity;
		Vector3 newVelocity = new Vector3(
			xVel * Time.deltaTime,
			currentVelocity.y, // keep the rigidbody's y velocity
			zVel * Time.deltaTime);
		rigidBody.velocity = newVelocity;
	}

	private void updateRotation() {

		float inputVector = -horzInput + balanceInput; // this value ranges from -2 to 2

		float playerRotation = transform.eulerAngles.z;
		if (playerRotation > 180) // player's rotation will be from -180 to 180 (0 is up, 180 and -180 is down)
			playerRotation -= 360;

		float rotateBy;

		if (horzInput == 0 && balanceInput == 0) { // only account for gravity when there is no player input
			rotateBy = rotationDueToGravity * playerRotation;
		} else { // player input takes over
			rotateBy = inputVector * tipsiness;
		}

		transform.RotateAround (transform.position, Vector3.forward, rotateBy * Time.deltaTime);

		/* commented out for now, prevent the player from tilting past a certain angle
		if (playerRotation > dropAtRotation) {
			Vector3 newRotation = new Vector3 (
				transform.eulerAngles.x,
				transform.eulerAngles.y,
				dropAtRotation);
			transform.rotation = Quaternion.Euler (newRotation);
		} else if (playerRotation < -dropAtRotation) {
			Vector3 newRotation = new Vector3 (
				transform.eulerAngles.x,
				transform.eulerAngles.y,
				360 - dropAtRotation);
			transform.rotation = Quaternion.Euler (newRotation);
		}
		*/
	}

	public void setXZvelocity(float xInput, float zInput) {
		xVel = Mathf.Lerp(xVel, strafeSpeed * xInput, 0.1f); // use Mathf.Lerp to smooth values
		zVel = Mathf.Lerp(zVel, forwardSpeed, 0.5f);
	}


	public void addJumpForce() {
		// and jump force in the world's up direction
		rigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse); // ForceMode.Impulse creates more of a "pop" when the force is applied
	}

	public int getCurrentLives() {
		return currentLives;
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Obstacle") {
			if (lifeLossCooldownCounter == 0) {
				lifeLossCooldownCounter = lifeLossCooldown;
				currentLives -= 1;
			}
		}

	}
}
