using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * This moves the player (by adding forces and velocities to the player's rigidbody) based on signals from the PlayerController.
 * 
 */

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{

    [SerializeField] private float playerForwardSpeed; // the player's run speed (Z direction)
    [SerializeField] private float playerStrafeSpeed; // the player's strafe speed (X direction)
    [SerializeField] private float handStrafeSpeed;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float cameraRotationSpeed;
    [SerializeField] private float rotationDueToStrafing;
    [SerializeField] private float rotationDueToGravity; // how much 'gravity' pulls the player over (not actually Rigidbody gravity)			
    [SerializeField] private float dropAtRotation; // how far the player can tip over before dropping the item

    // Player Bob Values
    [SerializeField] private float bobbingRate;
    [SerializeField] private float bobbingAmplitude;

    // Camera Bob Values
    [SerializeField] private float cameraBobRate;
    [SerializeField] private float cameraBobAmplitude;

    // Turning
    [SerializeField] private float turningSpeed;
	private bool canTurn = false;
	private bool canTurnLeft;
	private bool canTurnRight;
	private bool isTurning = false;
	private bool isTurningLeft;
    private float turnRadius;

    // Player life
    [SerializeField] private int currentLives = 5;
    [SerializeField] private int playerLives = 5;
    private bool isAlive = true;

    // Important objects used by player
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private SphereCollider playerHandParent;
    [SerializeField] private GameplayNotifier playerNotifier;
    private List<GameObject> obstaclesCollidedWith = new List<GameObject>();

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if (currentLives <= 0 || Mathf.Abs(getRollingRotation) > dropAtRotation)
        {
			playerNotifier.notify(new GameEvent(new List<GameObject>{this.gameObject}, GameEnumerations.EventCategory.Player_IsDead));
            isAlive = false;
        }

        // Get input from device
		float HorizontalInput;
		float BalanceInput;
        bool TurnRightInput;
        bool TurnLeftInput;
        getInput (out HorizontalInput, out BalanceInput, out TurnRightInput, out TurnLeftInput);

        // Move player, add rotation based on inputs and gravity
		movePlayer(HorizontalInput);
		addRollingRotationToHand(BalanceInput + (rotationDueToGravity * getRollingRotation));

        // Turning
        handleTurning(TurnRightInput, TurnLeftInput);

        // Notify that hands have rotated
        if (playerNotifier != null)
        {
            playerNotifier.notify(new GameEvent(new List<GameObject> { this.gameObject }, GameEnumerations.EventCategory.Player_HasRotatedHands));
        }
    }

    /**
     * movePlayer - Handle primary movement by the player
     * On Input:
     *     MovementAxisInput (float): Horizontal axis input recieved
     * On Output:
     *     Void, but has the following effects:
     *      - Strafing
     *      - Running Forward
     *      - Shifting Hands
     *      - Bobbing of Hands and Camera
     *      - Rotates the hands by a factor of rotationDueToStrafing
     * Call:
     *     float horizontal = Input.GetAxis("Horizontal");
     *     movePlayer(horizontal);
     */
    private void movePlayer(float MovementAxisInput)
    {
        Vector3 WalkingVector = playerHandParent.transform.forward * playerForwardSpeed * Time.deltaTime;

        // Bobbing Vectors
		Vector3 BobbingVector = playerHandParent.transform.up * bobbingAmplitude * bobbingRate * Mathf.Sin(Time.time * bobbingRate) * Time.deltaTime;
		Vector3 CameraBobbingVector = Vector3.up * cameraBobAmplitude * cameraBobRate * Mathf.Sin(Time.time * cameraBobRate) * Time.deltaTime;

        // Translation Vector
        Vector3 RightVector = Vector3.Cross(Vector3.up, playerHandParent.transform.forward);
        Vector3 StrafingVector = RightVector * MovementAxisInput * playerStrafeSpeed * Time.deltaTime;
        Vector3 ShiftingHandsVector = RightVector * handStrafeSpeed * MovementAxisInput * Time.deltaTime;

        // Move the player, bob the hand
        playerHandParent.transform.Translate(ShiftingHandsVector + BobbingVector, Space.World);
        this.transform.Translate(WalkingVector + StrafingVector, Space.World);

        // Camera Bob
        playerCamera.transform.Translate(CameraBobbingVector);

        // Add a small amount of rotation due to strafing
        addRollingRotationToHand(rotationDueToStrafing * MovementAxisInput);
    }

    /**
     * addRollingRotationToHand - Rotates the hands
     * On Input:
     *     RotationAxisInput (float): Rotation input recieved from player
     * On Output:
     *     Void, but effectively:
     *      - Rotates the hands, dictated by the sign of the input (Rolling rotation, Y)
     *        and the rotationSpeed
     *      - Rotates the camera by a factor of cameraRotationSpeed
     * Call:
     *     float rotation = Input.GetAxis("Rotation");
     *     addRollingRotationToHand(rotation);
     */
    private void addRollingRotationToHand(float RotationAxisInput)
    {
        playerHandParent.transform.Rotate(Vector3.forward * RotationAxisInput * rotationSpeed * Time.deltaTime);
        playerCamera.transform.Rotate(Vector3.forward * RotationAxisInput * cameraRotationSpeed * Time.deltaTime * -1.0f);
    }

    /**
     * turnPlayer - Rotates the player 90 degrees constantly by RotationSpeed
     * On Input:
     *     RotationSpeed (float): Speed at which the player rotates while turning
     * On Output:
     *     Void, but effectively:
     *      - Turns the player by a constant factor of RotationSpeed
     * Call:
     *     if (canTurn) {
     *       turnPlayer(50.0f);
     *     }
     */
    private void turnPlayer(float RotationSpeed)
    {
        float currentAngle = this.transform.rotation.eulerAngles.y;
        float deltaAngle = RotationSpeed * Time.deltaTime;
        float projectedAngle = currentAngle + deltaAngle;

        if (Mathf.Abs(turnRadius + deltaAngle) < 90.0f)
        {
            turnRadius += deltaAngle;
            this.transform.rotation = Quaternion.AngleAxis(projectedAngle, Vector3.up);
        }
        else
        {
            float remainingAngle = (RotationSpeed > 0) ? 90.0f - currentAngle % 90.0f : currentAngle % 90.0f * -1.0f;
            this.transform.rotation = Quaternion.AngleAxis(Mathf.Round(currentAngle + remainingAngle), Vector3.up);
            turnRadius = 0.0f;
            isTurning = false;
        }
    }

    /**
     * handleTurning - Determines if the player is allowed to turn and what direction
     * On Input:
     *     TurnRightPressed (bool): If the button for turning right was pressed
     *     TurnLeftPressed (bool): If the button for turning left was pressed
     * On Output:
     *     Void, but calls the turnPlayer method if one of the actions is pressed
     * Call:
     *     bool rightButtonPressed = GetButtonUp("TurnRight");
     *     bool leftButtonPressed = GetButtonUp("TurnRight");
     *     handleTurning(rightButtonPressed, leftButtonPressed);
     */
    private void handleTurning(bool TurnRightPressed, bool TurnLeftPressed)
    {
        if (canTurn)
        {
            if (canTurnRight && TurnRightPressed)
            {
                isTurning = true;
                isTurningLeft = false;
            }

            if (canTurnLeft && TurnLeftPressed)
            {
                isTurning = true;
                isTurningLeft = true;
            }
        }

        if (isTurning)
        {
            float direction = (isTurningLeft) ? -1.0f : 1.0f;
            turnPlayer(direction * turningSpeed);
        }
    }

    /**
     * OnCollisionEnter - Handle physical collisions with obstacles
     * On Output:
     *     If the collider was an Obstacle (determined by its tag), notify the
     *     event system that the player recieved a hit
     */
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle" &&
            !obstaclesCollidedWith.Contains(col.gameObject))
        {
            obstaclesCollidedWith.Add(col.gameObject);
            playerNotifier.notify(new GameEvent(new List<GameObject> { this.gameObject }, GameEnumerations.EventCategory.Player_IsHurt));
            return;
        }

        if (col.gameObject.GetComponent<Pickup>() != null)
        {
            return;
        }
    }

    /**
     * OnTriggerEnter - Handle trigger events when first entered
     * On Output:
     *     If the collider was a turning volume, then notify the event system so
     */
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<TurningVolume>() != null)
        {
            TurningVolume turn = col.gameObject.GetComponent<TurningVolume>();
            if (turn.canTurnLeft)
            {
                canTurnLeft = true;
                playerNotifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Player_CanTurnLeft));
            }

            if (turn.canTurnRight)
            {
                canTurnRight = true;
                playerNotifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Player_CanTurnRight));
            }

            this.canTurn = true;
            return;
        }
    }

    /**
     * OnTriggerExit  - Handle trigger events when exited
     * On Output:
     *     Disable all turning elements
     */
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<TurningVolume>() != null)
        {
            this.canTurn = false;
            this.canTurnRight = false;
            this.canTurnLeft = false;
            return;
        }
    }

    /**
     * getInput - Handle all input from all supported input mechanisms
     * On Input:
     *     HorizontalInput (float): Horizontal input axis
     *     BalanceInput (float): Rotation input axis
     *     TurnRightInput (bool): Input for turning to the right
     *     TurnLeftInput (bool): Input for turning to the left
     * On Output:
     *     All parameters inputed, set to thier respective values
     * Call:
     *     float horizontalInput;
     *     float balanceInput;
     *     bool turnRightInput;
     *     bool turnLeftInput;
     *     getInput (out horizontalInput, out balanceInput, out turnRightInput, out turnLeftInput);
     */
    private void getInput(out float HorizontalInput, out float BalanceInput, 
                          out bool TurnRightInput, out bool TurnLeftInput) 
	{
		HorizontalInput = Input.GetAxis("Horizontal");
		BalanceInput = Input.GetAxis("Rotation");
        TurnRightInput = Input.GetButtonUp("TurnRight");
        TurnLeftInput = Input.GetButtonUp("TurnLeft");

        // Gyroscope input
        if (Mathf.Abs (Input.gyro.rotationRate.z) > 0.1f) 
		{
			BalanceInput = -1.0f * Input.gyro.rotationRate.z * 2.5f;
		}
		
		// Touch
		if (Input.touches.Length == 1) 
		{
			Touch t1 = Input.GetTouch (0);
			HorizontalInput = (t1.position.x < Screen.width / 2) ? -1 : 1;
		}
	}

    /**
     * getCurrentLives - Get Current Lives
     */
    public int getCurrentLives()
    {
        return currentLives;
    }

    /**
     * getTotalLives - Get total lives
     */
    public int getTotalLives()
    {
        return playerLives;
    }

    /**
     * getPlayerAlive - Is the player alive?
     */
    public bool getPlayerAlive()
    {
        return isAlive;
    }

    /**
     * getRollingRotation - Get Rolling rotation of player
     */
    public float getRollingRotation
    {
        get
        {
            return (Mathf.Abs(playerHandParent.transform.eulerAngles.z) < 0.0001f) ? 0  : 
                NormalizeAngle(playerHandParent.transform.eulerAngles.z);
        }
    }

    /**
    * NormalizeAngle - Normalizes angles, used for UI balance meter
    */
    private float NormalizeAngle(float angle)
    {
        return (angle > 180.0f) ? (angle - 360.0f) : angle;
    }

    /**
     * RecieveDamage - Take Damage
     * On Input:
     *     damage (int): Damage amount
     * On Output:
     *     currentLives decreased by a set amount
     * Call:
     *     RecieveDamage(1);
     */
    public void RecieveDamage(int damage)
    {
        currentLives -= damage;
    }
}
