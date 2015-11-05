using UnityEngine;
using System.Collections;

/*
 * This moves the player (by adding forces and velocities to the player's rigidbody) based on signals from the PlayerController.
 * 
 */

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private float playerForwardSpeed; // the player's run speed (Z direction)
    [SerializeField]
    private float playerStrafeSpeed; // the player's strafe speed (X direction)
    [SerializeField]
    private float handStrafeSpeed;

    public float rotationSpeed;
    public float rotationDueToStrafing;
    public float rotationDueToGravity; // how much 'gravity' pulls the player over (not actually Rigidbody gravity)			
    public float dropAtRotation; // how far the player can tip over before dropping the item

    [SerializeField]
    private float P_BobbingRate;
    [SerializeField]
    private float P_BobbingAmplitude;
    private float P_LocalBobbingTime;

    //control inputs
    private float horzInput, balanceInput;

    // camera bob values
    public float cameraBobRate;
    public float cameraBobAmplitude;
    private float cameraStartHeight;

    //player life
    private bool isAlive;
    public int playerLives = 5;
    private int currentLives;
    public float lifeLossCooldown; // how quickly the player can lose life, so they don't lose all their lives at once
    private float lifeLossCooldownCounter = 0f;

    //Important objects used by player
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private SphereCollider playerHandParent;
    [SerializeField]
    private GameplayNotifier playerNotifier;

    //Initializes our player
    void Start()
    {
        isAlive = true;
        cameraStartHeight = playerCamera.transform.localPosition.y;
        currentLives = playerLives;
    }

    //use this for stuff like inputs
    void Update()
    {
        horzInput = Input.GetAxis("Horizontal");
        balanceInput = Input.GetAxis("Balance");

        if (Mathf.Abs(balanceInput) < 0.1f)
            balanceInput = Input.GetAxis("Rotation");

        //STUPID way top check if we should stop. should probablt be changed
        if (isAlive)
        {
            lifeHandler();
            movePlayer(horzInput);
            addRollingRotationToHand(balanceInput);
            addRollingRotationToHand(rotationDueToGravity * getRollingRotation);
            playerNotifier.notify(new GameEvent(this.gameObject, GameEnumerations.EventCategory.Player_HasRotatedHands));
            updateCamera();
        }
        else
        {
            playerNotifier.notify(new GameEvent(this.gameObject, GameEnumerations.EventCategory.Player_IsDead));
        }

    }

    private void updateCamera()
    {
        float newHeight = cameraStartHeight + (cameraBobAmplitude * Mathf.Sin(cameraBobRate * Time.time));
        playerCamera.transform.localPosition = new Vector3(
            playerCamera.transform.localPosition.x,
            newHeight,
            playerCamera.transform.localPosition.z);
    }

    private void movePlayer(float MovementAxisInput)
    {
        Vector3 WalkingVector = Vector3.forward * playerForwardSpeed * Time.deltaTime;
        Vector3 StrafingVector = Vector3.right * MovementAxisInput * playerStrafeSpeed * Time.deltaTime;

        // Bobbing Vector
        P_LocalBobbingTime += 1 / P_BobbingRate;
        Vector3 BobbingVector = Vector3.up * P_BobbingAmplitude * Mathf.Sin(P_LocalBobbingTime);


        // Translation Vector
        Vector3 ShiftingHandsVector = Vector3.right * handStrafeSpeed * MovementAxisInput * Time.deltaTime;
        this.transform.Translate(WalkingVector + StrafingVector);
        //playerHandParent.transform.Translate (BobbingVector + ShiftingHandsVector, Space.World);

        addRollingRotationToHand(rotationDueToStrafing * MovementAxisInput);
    }

    private void addRollingRotationToHand(float RotationAxisInput)
    {
        playerHandParent.transform.Rotate(Vector3.right * RotationAxisInput * rotationSpeed * Time.deltaTime);
        playerCamera.transform.Rotate(Vector3.forward * RotationAxisInput * rotationDueToStrafing * Time.deltaTime * 50.0f);
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
    private void lifeHandler()
    {
        if (lifeLossCooldownCounter > 0)
        {
            lifeLossCooldownCounter -= Time.deltaTime;
            if (lifeLossCooldownCounter < 0) lifeLossCooldownCounter = 0;
        }

        if (currentLives <= 0 || Mathf.Abs(getRollingRotation) > dropAtRotation)
        {
            //P_Notifier.notify (new GameEvent (this.gameObject, GameEnumerations.EventCategory.Player_IsDead));
            isAlive = false;
        }
    }


    //gets the rotation of the players hands
    public float getRollingRotation
    {
        get { return NormalizeAngle(playerHandParent.transform.eulerAngles.x); }
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

        if (col.gameObject.tag == "Obstacle")
        {
            if (lifeLossCooldownCounter == 0)
            {
                lifeLossCooldownCounter = lifeLossCooldown;
                playerNotifier.notify(new GameEvent(this.gameObject, GameEnumerations.EventCategory.Player_IsHurt));
            }
        }
    }
}
