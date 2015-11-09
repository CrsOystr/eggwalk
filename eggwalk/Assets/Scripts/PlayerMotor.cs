using UnityEngine;
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

    [SerializeField] private float bobbingRate;
    [SerializeField] private float bobbingAmplitude;

    // camera bob values
    [SerializeField] private float cameraBobRate;
    [SerializeField] private float cameraBobAmplitude;

    //player life
    private bool isAlive = true;
    [SerializeField] private int currentLives = 5;
    [SerializeField] private int playerLives = 5;

    //Important objects used by player
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private SphereCollider playerHandParent;
    [SerializeField] private GameplayNotifier playerNotifier;

    private List<GameObject> obstaclesCollidedWith;

    //Initializes our player
    void Start()
    {
        obstaclesCollidedWith = new List<GameObject>();
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if (currentLives <= 0 || Mathf.Abs(getRollingRotation) > dropAtRotation)
        {
            playerNotifier.notify(new GameEvent(this.gameObject, GameEnumerations.EventCategory.Player_IsDead));
            isAlive = false;
        }

		float HorizontalInput;
		float BalanceInput;
		getInput (out HorizontalInput, out BalanceInput);

        // Move player, add rotation based on inputs and gravity
		movePlayer(HorizontalInput);
		addRollingRotationToHand(BalanceInput + (rotationDueToGravity * getRollingRotation));

        // Notify that hands have rotated
        playerNotifier.notify(new GameEvent(this.gameObject, GameEnumerations.EventCategory.Player_HasRotatedHands));
    }

    private void movePlayer(float MovementAxisInput)
    {
        Vector3 WalkingVector = Vector3.forward * playerForwardSpeed * Time.deltaTime;
        Vector3 StrafingVector = Vector3.right * MovementAxisInput * playerStrafeSpeed * Time.deltaTime;

        // Bobbing Vectors
        Vector3 BobbingVector = Vector3.up * bobbingAmplitude * Mathf.Sin(Time.time * bobbingRate);
        Vector3 CameraBobbingVector = Vector3.up * cameraBobAmplitude * Mathf.Sin(Time.time * cameraBobRate);

        // Translation Vector
        Vector3 ShiftingHandsVector = Vector3.right * handStrafeSpeed * MovementAxisInput * Time.deltaTime;

        // Move the player, bob the hand
        playerHandParent.transform.Translate(BobbingVector + ShiftingHandsVector, Space.World);
        this.transform.Translate(WalkingVector + StrafingVector);

        // Camera Bob
        playerCamera.transform.Translate(CameraBobbingVector);

        // Add a small amount of rotation due to strafing
        addRollingRotationToHand(rotationDueToStrafing * MovementAxisInput);
    }

    private void addRollingRotationToHand(float RotationAxisInput)
    {
        playerHandParent.transform.Rotate(Vector3.right * RotationAxisInput * rotationSpeed * Time.deltaTime);
        playerCamera.transform.Rotate(Vector3.forward * RotationAxisInput * rotationDueToStrafing * Time.deltaTime * cameraRotationSpeed);
    }

	// Get inputs
	private void getInput(out float HorizontalInput, out float BalanceInput) 
	{
		HorizontalInput = Input.GetAxis("Horizontal");
		BalanceInput = Input.GetAxis("Rotation");
		
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

    public int getCurrentLives()
    {
        return currentLives;
    }

    public int getTotalLives()
    {
        return playerLives;
    }
    public bool getPlayerAlive()
    {
        return isAlive;
    }

    //gets the rotation of the players hands
    public float getRollingRotation
    {
        get
        {
            return (playerHandParent.transform.eulerAngles.x < 0.0001f) ? 0  : 
                NormalizeAngle(playerHandParent.transform.eulerAngles.x);
        }
    }

    //normalizes angles
    private float NormalizeAngle(float angle)
    {
        return (angle > 180.0f) ? (angle - 360.0f) : angle;
    }

    public void RecieveDamage(int damage)
    {
        currentLives -= damage;
    }

    //handles collison detection
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Obstacle" && 
            !obstaclesCollidedWith.Contains(col.gameObject))
        {
            obstaclesCollidedWith.Add(col.gameObject);
            playerNotifier.notify(new GameEvent(this.gameObject, GameEnumerations.EventCategory.Player_IsHurt));
        }
    }
}
