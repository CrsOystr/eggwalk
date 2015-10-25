using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	[SerializeField] private float P_WalkingSpeed;
	[SerializeField] private float P_StrafingSpeed;
	[SerializeField] private float P_RotationSpeed;
	[SerializeField] private float P_RotationDueToStrafing;
	[SerializeField] private float P_BobbingRate;
	[SerializeField] private float P_BobbingAmplitude;
	[SerializeField] private float P_MaxRotation;
	[SerializeField] private int TotalLives;
	[SerializeField] private int CurrentLives;
	private float P_LocalBobbingTime;
	[SerializeField] private SphereCollider P_HandParent;
	[SerializeField] private Camera P_Camera;
	[SerializeField] private GameplayNotifier P_Notifier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (P_CurrentLives <= 0) {
			P_Notifier.notify (new GameEvent (this.gameObject, GameEnumerations.EventCategory.Player_IsDead));
			return;
		}

		float HorizontalAxisInput = Input.GetAxis ("Horizontal");
		float RotationAxisInput = Input.GetAxis ("Rotation");

		if (Input.GetKeyUp (KeyCode.P)) {
			P_Notifier.notify (new GameEvent (this.gameObject, GameEnumerations.EventCategory.Player_IsHurt));
		}

		MoveRight (HorizontalAxisInput);
		AddRollingRotationToHand (RotationAxisInput);
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

		// Move the player, bob the HandParent
		transform.Translate (WalkingVector + StrafingVector);
		P_HandParent.transform.Translate (BobbingVector);

		AddRollingRotationToHand (P_RotationDueToStrafing * MovementAxisInput);
	}

	private void AddRollingRotationToHand(float RotationAxisInput) {
		P_HandParent.transform.Rotate (Vector3.right * RotationAxisInput * P_RotationSpeed * Time.deltaTime);
		P_Camera.transform.Rotate (Vector3.forward * RotationAxisInput * P_RotationDueToStrafing * Time.deltaTime);
	}

	public void RecieveDamage(int damage) {
		CurrentLives -= damage;
	}

	public int P_TotalLives {
		get { return TotalLives; }
	}

	public int P_CurrentLives {
		get { return CurrentLives; }
		private set { CurrentLives = value; }
	}
}
