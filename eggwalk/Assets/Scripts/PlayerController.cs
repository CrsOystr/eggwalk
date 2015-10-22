using UnityEngine;
using System.Collections;

/*
 * This class takes in input and sends signals to the PlayerMotor.
 * 
 * PlayerController = handles input
 * PlayerMotor = actually moves the player
 */


[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

	[SerializeField] GameObject groundTrigger; // ground trigger will detect when the player is standing on something

	private PlayerMotor motor;

	void Start () {
		motor = GetComponent<PlayerMotor> ();
	}

	void Update () {
		// initilize inputs to zero
		float horzInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");


		
		motor.setXZvelocity (horzInput, vertInput);

		// jump only when on the ground
		if (groundTrigger.GetComponent<GroundTriggerBehavior>().isOnGround() && Input.GetButton ("Jump"))
			motor.addJumpForce();

	}
}
