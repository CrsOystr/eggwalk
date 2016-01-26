using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * This moves the player (by adding forces and velocities to the player's rigidbody) based on signals from the PlayerController.
 * 
 */

[RequireComponent (typeof (Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private PlayerStats activePlayerStats;

    // Important objects used by player
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private SphereCollider playerHandParent;
    [SerializeField]
    private GameplayNotifier playerNotifier;
    [SerializeField]
    private Transform itemLocation;
    [SerializeField]
    private Arrow arrow;

    private PlayerStats originalPlayerStats;
    private bool playerCanStart = false;
	private bool canTurn = false;
	private bool canTurnLeft;
	private bool canTurnRight;
	private bool isTurning = false;
	private bool isTurningLeft;
    private float turnRadius;
    private bool buffed;
    private bool buffedSpeed;
    private bool isAlive = true;
    private GameObject heldItem;

    void Start()
    {
        this.originalPlayerStats = this.activePlayerStats;
        this.arrow.setActive(true);
        List<GameObject> entityMessage = new List<GameObject>{ this.gameObject };
        this.playerNotifier.notify(new GameEvent(entityMessage, GameEnumerations.EventCategory.Gameplay_InitializeHUD));

        this.buffed = true;
        StartCoroutine(initialBuffer());
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if (activePlayerStats.CurrentLives <= 0 || Mathf.Abs(getRollingRotation) > activePlayerStats.DropAtRotation)
        {
            if (isAlive)
            {
                playerNotifier.notify(new GameEvent(new List<GameObject> { this.gameObject }, GameEnumerations.EventCategory.Player_IsDead));
                isAlive = false;
            }
        }

        // Get input from device
		float HorizontalInput;
		float BalanceInput;
        bool TurnRightInput;
        bool TurnLeftInput;
        getInput (out HorizontalInput, out BalanceInput, out TurnRightInput, out TurnLeftInput);

        // Move player, add rotation based on inputs and gravity
		movePlayer(HorizontalInput);
		addRollingRotationToHand(BalanceInput + (activePlayerStats.RotationDueToGravity * getRollingRotation));

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
        float baseSpeed = (this.playerCanStart) ? 1.0f : 0.0f;

        Vector3 WalkingVector = playerHandParent.transform.forward * activePlayerStats.ForwardSpeed * Time.deltaTime * baseSpeed;

        if (this.buffedSpeed)
        {
            WalkingVector = 0.5f * WalkingVector;
        }

        // Translation Vector
        Vector3 RightVector = Vector3.Cross(Vector3.up, playerHandParent.transform.forward);
        Vector3 StrafingVector = RightVector * MovementAxisInput * activePlayerStats.StrafeSpeed * Time.deltaTime * baseSpeed;

        // Bobbing Vectors
        Vector3 BobbingVector = playerHandParent.transform.up * activePlayerStats.BobbingAmplitude * activePlayerStats.BobbingRate * Mathf.Sin(Time.time * activePlayerStats.BobbingRate) * Time.deltaTime * baseSpeed;
		Vector3 CameraBobbingVector = Vector3.up * activePlayerStats.CameraBobAmplitude * activePlayerStats.CameraBobRate * Mathf.Sin(Time.time * activePlayerStats.CameraBobRate) * Time.deltaTime * baseSpeed;
        Vector3 ShiftingHandsVector = RightVector * activePlayerStats.HandStrafeSpeed * MovementAxisInput * Time.deltaTime * baseSpeed;

        // Move the player, bob the hand
        playerHandParent.transform.Translate(ShiftingHandsVector + BobbingVector, Space.World);
        this.transform.Translate(WalkingVector + StrafingVector, Space.World);

        // Camera Bob
        playerCamera.transform.Translate(CameraBobbingVector);

        // Add a small amount of rotation due to strafing
        addRollingRotationToHand(activePlayerStats.RotationDueToStrafing * MovementAxisInput);
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
        if (heldItem != null)
        {
            playerHandParent.transform.Rotate(Vector3.forward * RotationAxisInput * activePlayerStats.RotationSpeed * Time.deltaTime);
            playerCamera.transform.Rotate(Vector3.forward * RotationAxisInput * activePlayerStats.CameraRotationSpeed * Time.deltaTime * -1.0f);
        }
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
                playerNotifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Player_IsTurningRight));
            }

            if (canTurnLeft && TurnLeftPressed)
            {
                isTurning = true;
                isTurningLeft = true;
                playerNotifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Player_IsTurningLeft));
            }
        }

        if (isTurning)
        {
            float direction = (isTurningLeft) ? -1.0f : 1.0f;
            turnPlayer(direction * activePlayerStats.TurningSpeed);
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
        if (col.gameObject.tag == "Obstacle" && !buffed)
        {
            playerNotifier.notify(new GameEvent(new List<GameObject> { this.gameObject }, GameEnumerations.EventCategory.Player_IsHurt));
            return;
		}


        if (col.gameObject.tag.Equals("Building"))
        {
            if (isAlive)
            {
                float dir = Vector3.Dot(col.gameObject.transform.forward, this.transform.forward);
                if (dir < -0.97)
                {
                    //this.transform.localRotation = Quaternion.AngleAxis(this.transform.rotation.eulerAngles.y + 90.0f, Vector3.up);
                    if (!buffed)
                    {
                        isTurning = true;

                        Ray rightRay = new Ray(this.transform.position, this.transform.right);
                        Ray leftRay = new Ray(this.transform.position, -1.0f * this.transform.right);
                        RaycastHit rightHit;
                        RaycastHit leftHit;

                        Physics.Raycast(rightRay, out rightHit, 1 << 8);
                        Physics.Raycast(leftRay, out leftHit, 1 << 8);

                        isTurningLeft = (leftHit.distance > rightHit.distance);

                        playerNotifier.notify(new GameEvent(new List<GameObject> { this.gameObject }, GameEnumerations.EventCategory.Player_IsHurt));
                    }
                    //isAlive = false;
                }
            }
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

            if (turn.canTurnLeft(this.transform.forward))
            {
                canTurnLeft = true;
                playerNotifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Player_CanTurnLeft));
            }

            if (turn.canTurnRight(this.transform.forward))
            {
                canTurnRight = true;
                playerNotifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Player_CanTurnRight));
            }

            this.canTurn = true;
            col.gameObject.GetComponent<TurningVolume>().IsPlayerTurning = true;

            return;
        }

        if (col.gameObject.GetComponent<TriggerBox>() != null)
        {
            TriggerBox trigger = col.gameObject.GetComponent<TriggerBox>();
            if (this.heldItem != null && trigger.Activated)
            {
                List<GameObject> entityMessage = new List<GameObject>() { this.gameObject, col.gameObject };
                playerNotifier.notify(new GameEvent(entityMessage, GameEnumerations.EventCategory.Player_ReturnedTarget));
            }

            return;
        }

        if (col.gameObject.GetComponent<Pickup>() != null)
        {
            if (heldItem == null)
            {
                GameObject pickup = col.gameObject;
                List<GameObject> entityMessage = new List<GameObject>() { this.gameObject, pickup };

                //addItemIntoHand(col.gameObject.GetComponent<Pickup>().getTargetItem());
                playerNotifier.notify(new GameEvent(entityMessage, GameEnumerations.EventCategory.Player_StartedObjective));
            }
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
            col.gameObject.GetComponent<TurningVolume>().IsPlayerTurning = false;

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
        TurnRightInput = Input.GetButtonDown("TurnRight");
        TurnLeftInput = Input.GetButtonDown("TurnLeft");

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
     * addItemIntoHand - Add a gameObject into the players hand, and set as active item
     * On Output:
     *    If the item was successfully added, if there is already an item in hand, false
     * Call:
     *    Player player = ...; // Reference to player
     *    GameObject prefab = ....; // Some prefab provided
     *    Transform t = ....;
     *    GameObject newItem = Instantiate(prefab, t.position, t.rotation);
     *    bool successfullyAdded = player.addItemToHand(newItem);
     *    if (successfullyAdded) {
     *      Debug.Log("Player has a new Item!");
     *    } else {
     *      Debug.Log("Dummy, you already have an item!");
     *    }
     */
    public bool addItemIntoHand(GameObject targetItem)
    {
        if (this.heldItem != null)
        {
            return false;
        }

        // Apply effects
        Pickup pickup = targetItem.GetComponent<Pickup>();
        modPlayerAttributes(pickup.getModifiers());

        // Reparent item, and reposition into players hand
        this.heldItem = targetItem;
        targetItem.transform.parent = playerHandParent.transform;
        targetItem.transform.localPosition = this.itemLocation.localPosition;
        targetItem.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        return true;
    }

    public bool removeItemFromHand()
    {
        if (this.heldItem == null)
        {
            return false;
        }

        revertAllAttributes();

        this.heldItem = null;
        return true;
    }

    private IEnumerator initialBuffer()
    {
        yield return new WaitForSeconds(activePlayerStats.InitialBuffingTime);
        this.buffed = false;
    }

    private IEnumerator removeBuffer()
    {
        yield return new WaitForSeconds(activePlayerStats.BuffingTime);
        this.buffed = false;
        this.buffedSpeed = false;
    }

    public void bumpPlayer()
    {
        float noise = UnityEngine.Random.Range(-20.0f, 20.0f);
        addRollingRotationToHand(noise);
    }

    public bool resetLives()
    {
        if (!isAlive)
        {
            return false;
        } else
        {
            activePlayerStats.CurrentLives = activePlayerStats.PlayerLives;
            return true;
        }
    }

    public void onDeath()
    {
        this.arrow.setActive(false);
        this.GetComponent<Rigidbody>().freezeRotation = false;
        this.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000.0f);
        this.GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
        this.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
    }

    public void enableArrow(bool val)
    {
        this.arrow.setActive(val);
    }

    public GameObject getItemInHand()
    {
        return heldItem;
    }

    /**
     * getCurrentLives - Get Current Lives
     */
    public int getCurrentLives()
    {
        return activePlayerStats.CurrentLives;
    }

    /**
     * getTotalLives - Get total lives
     */
    public int getTotalLives()
    {
        return activePlayerStats.PlayerLives;
    }

    /**
     * getPlayerAlive - Is the player alive?
     */
    public bool getPlayerAlive()
    {
        return isAlive;
    }

    /**
     * getRollingRotation - Get Rolling rotation of player hands
     */
    public float getRollingRotation
    {
        get
        {
            return (Mathf.Abs(playerHandParent.transform.eulerAngles.z) < 0.0001f) ? 0  : 
                NormalizeAngle(playerHandParent.transform.eulerAngles.z);
        }
    }

    public void startPlayer(bool val)
    {
        this.playerCanStart = val;
    }

    public void returnToNeutral()
    {
        playerCamera.transform.localRotation = Quaternion.AngleAxis(0.0f, playerCamera.transform.up);
        playerHandParent.transform.localRotation = Quaternion.AngleAxis(0.0f, playerHandParent.transform.up);
    }

    public void setTarget(Transform newDestination)
    {
        this.arrow.WorldDestination = newDestination;
    }

    /**
     * modPlayerAttribute - Modify a players attributes based on a specific modifier
    */
    public void modPlayerAttribute(PickupModifier mod)
    {
        // Revert first
        revertPlayerAttribute(mod);

        GameEnumerations.PlayerModifier type = mod.modifier;
        switch (type)
        {
            case GameEnumerations.PlayerModifier.Player_Mod_Speed:
                {
                    activePlayerStats.ForwardSpeed *= mod.value;
                    break;
                }
            case GameEnumerations.PlayerModifier.Player_Mod_Balance:
                {
                    activePlayerStats.RotationSpeed *= mod.value;
                    break;
                }
        }
    }

    public void modPlayerAttributes(List<PickupModifier> mods)
    {
        for (int i = 0; i < mods.Count; i++)
        {
            modPlayerAttribute(mods[i]);
        }
    }

    /**
     * revertPlayerAttribute - Revert the players attributes back to normal 
     *                         based on a known modifier 
    */
    public void revertPlayerAttribute(PickupModifier mod)
    {
        GameEnumerations.PlayerModifier type = mod.modifier;
        switch (type)
        {
            case GameEnumerations.PlayerModifier.Player_Mod_Speed:
                {
                    activePlayerStats.ForwardSpeed = activePlayerStats.ForwardSpeed / mod.value;
                    break;
                }
            case GameEnumerations.PlayerModifier.Player_Mod_Balance:
                {
                    activePlayerStats.RotationSpeed = activePlayerStats.RotationSpeed / mod.value;
                    break;
                }
        }
    }

    public void revertAllAttributes()
    {
        this.activePlayerStats = this.originalPlayerStats;
    }

    public void revertPlayerAttribute(GameEnumerations.PlayerModifier type)
    {
        switch (type)
        {
            case GameEnumerations.PlayerModifier.Player_Mod_Speed:
                {
                    activePlayerStats.ForwardSpeed = activePlayerStats.ForwardSpeed / originalPlayerStats.ForwardSpeed;
                    break;
                }
            case GameEnumerations.PlayerModifier.Player_Mod_Balance:
                {
                    activePlayerStats.RotationSpeed = activePlayerStats.RotationSpeed / originalPlayerStats.RotationSpeed;
                    break;
                }
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
        this.buffed = true;
        this.buffedSpeed = true;
        activePlayerStats.CurrentLives -= damage;
        StartCoroutine(removeBuffer());
    }

    [System.Serializable]
    public class PlayerStats
    {
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float strafeSpeed;
        [SerializeField] private float handStrafeSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float cameraRotationSpeed;
        [SerializeField] private float rotationDueToStrafing;
        [SerializeField] private float rotationDueToGravity;
        [SerializeField] private float dropAtRotation;
        [SerializeField] private float bobbingRate;
        [SerializeField] private float bobbingAmplitude;
        [SerializeField] private float cameraBobRate;
        [SerializeField] private float cameraBobAmplitude;
        [SerializeField] private float turningSpeed;
        [SerializeField] private int currentLives;
        [SerializeField] private int playerLives;
        [SerializeField] private float initialBuffingTime = 10.0f;
        [SerializeField] private float buffingTime = 2.0f;
        [SerializeField] private float buffedFactor = 0.5f;

        public float ForwardSpeed
        {
            get { return this.forwardSpeed; }
            set { this.forwardSpeed = value; }
        }

        public float StrafeSpeed
        {
            get { return this.strafeSpeed; }
        }

        public float HandStrafeSpeed
        {
            get { return this.handStrafeSpeed; }
        }

        public float RotationSpeed
        {
            get { return this.rotationSpeed; }
            set { this.rotationSpeed = value; }
        }

        public float CameraRotationSpeed
        {
            get { return this.cameraRotationSpeed; }
        }

        public float RotationDueToStrafing
        {
            get { return this.rotationDueToStrafing; }
        }

        public float RotationDueToGravity
        {
            get { return this.rotationDueToGravity; }
        }

        public float DropAtRotation
        {
            get { return this.dropAtRotation; }
        }

        public float BobbingRate
        {
            get { return this.bobbingRate; }
        }

        public float BobbingAmplitude
        {
            get { return this.bobbingAmplitude; }
        }

        public float CameraBobRate
        {
            get { return this.cameraBobRate; }
        }

        public float CameraBobAmplitude
        {
            get { return this.cameraBobAmplitude; }
        }

        public float TurningSpeed
        {
            get { return this.turningSpeed; }
        }

        public int CurrentLives
        {
            get { return this.currentLives; }
            set { this.currentLives = value; }
        }

        public int PlayerLives
        {
            get { return this.playerLives; }
        }

        public float InitialBuffingTime
        {
            get { return this.initialBuffingTime; }
        }

        public float BuffingTime
        {
            get { return this.buffingTime; }
        }

        public float BuffedFactor
        {
            get { return this.buffedFactor; }
        }
    }
}
