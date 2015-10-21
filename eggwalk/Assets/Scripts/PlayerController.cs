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

	private PlayerMotor motor;

	void Start () {
		motor = GetComponent<PlayerMotor> ();
	}

	void Update () {
		// initilize inputs to zero
		float horzInput = 0;
		float vertInput = 0;

		// input for keyboard control
		// button input can be -1, 0, or 1
		if (Input.GetButton ("W"))
			vertInput = 1.0f;
		if (Input.GetButton ("S"))
			vertInput = -1.0f;
		if (Input.GetButton ("D"))
			horzInput = 1.0f;
		if (Input.GetButton ("A"))
			horzInput = -1.0f;

		motor.setXZvelocity (horzInput, vertInput);

		// jump!
		if (Input.GetButton ("Jump"))
			motor.addJumpForce();

	}
}
