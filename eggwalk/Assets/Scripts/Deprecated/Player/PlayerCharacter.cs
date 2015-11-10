using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour {

	[SerializeField] private float P_WalkingSpeed;
	[SerializeField] private float P_StrafingSpeed;
    [SerializeField] private float P_HandStrafingSpeed;
    [SerializeField] private float P_RotationSpeed;
	[SerializeField] private float P_RotationDueToStrafing;
    [SerializeField] private float P_RotationDueToGravity;
    [SerializeField] private float P_BobbingRate;
	[SerializeField] private float P_BobbingAmplitude;
	[SerializeField] private float P_MaxRotation;
	[SerializeField] private int TotalLives;
	[SerializeField] private int CurrentLives;
	private float P_LocalBobbingTime;

	[SerializeField] private SphereCollider P_HandParent;
	[SerializeField] private Camera P_Camera;
	[SerializeField] private GameplayNotifier P_Notifier;
    private bool P_IsAlive;

	// Use this for initialization
	void Start () {
        this.P_IsAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!P_IsAlive) {
            return;
        }

		if (P_CurrentLives <= 0 || Mathf.Abs(P_RollingRotation) > P_MaxRotation) {
			P_Notifier.notify (new GameEvent (new List<GameObject> {this.gameObject}, GameEnumerations.EventCategory.Player_IsDead));
            P_IsAlive = false;
			return;
		}

		float HorizontalAxisInput = Input.GetAxis ("Horizontal");
		float RotationAxisInput = Input.GetAxis ("Rotation");

		if (Input.GetKeyUp (KeyCode.P)) {
			P_Notifier.notify (new GameEvent (new List<GameObject>{this.gameObject}, GameEnumerations.EventCategory.Player_IsHurt));
		}

		MoveRight (HorizontalAxisInput);
		AddRollingRotationToHand (RotationAxisInput);
        AddRollingRotationToHand(P_RotationDueToGravity * P_RollingRotation);

		P_Notifier.notify(new GameEvent(new List<GameObject>{this.gameObject}, GameEnumerations.EventCategory.Player_HasRotatedHands));
    }

	/**
	 * MoveRight
	 * Inputs: 
	 */
	private void MoveRight(float MovementAxisInput) {
		Vector3 WalkingVector = Vector3.forward * P_WalkingSpeed * Time.deltaTime;
		Vector3 StrafingVector = Vector3.right * MovementAxisInput * P_StrafingSpeed * Time.deltaTime;

		// Bobbing Vector
		P_LocalBobbingTime += 1 / P_BobbingRate;
		Vector3 BobbingVector = Vector3.up * P_BobbingAmplitude * Mathf.Sin(P_LocalBobbingTime);

        // Translation Vector
        Vector3 ShiftingHandsVector = Vector3.right * P_HandStrafingSpeed * MovementAxisInput * Time.deltaTime;

		// Move the player, bob the HandParent
		transform.Translate (WalkingVector + StrafingVector);
		P_HandParent.transform.Translate (BobbingVector + ShiftingHandsVector, Space.World);

        AddRollingRotationToHand (P_RotationDueToStrafing * MovementAxisInput);
	}

	private void AddRollingRotationToHand(float RotationAxisInput) {
		P_HandParent.transform.Rotate (Vector3.forward * RotationAxisInput * P_RotationSpeed * Time.deltaTime * -1.0f);
		P_Camera.transform.Rotate (Vector3.forward * RotationAxisInput * P_RotationDueToStrafing * Time.deltaTime * 50.0f);
	}

	public void RecieveDamage(int damage) {
		CurrentLives -= damage;
	}

    /**
      *  
      */
    private float NormalizeAngle(float angle)
    {
        return (angle > 180.0f) ? (angle - 360.0f) : angle;
    }

	public int P_TotalLives {
		get { return TotalLives; }
	}

	public int P_CurrentLives {
		get { return CurrentLives; }
		private set { CurrentLives = value; }
	}

    public float P_RollingRotation {
        get { return NormalizeAngle(P_HandParent.transform.eulerAngles.x); }
    }
}
