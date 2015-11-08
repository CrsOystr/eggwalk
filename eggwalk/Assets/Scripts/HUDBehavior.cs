using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDBehavior : MonoBehaviour {

	public GameObject player;
	public GameObject playerHands;
	public GameObject balanceMeter;
	public GameObject balanceArrow;
	public GameObject livesText;
	public GameObject restartButton;

	private PlayerMotor playerMotor;

	void Start() {
		playerMotor = player.GetComponent<PlayerMotor> ();
	}

	void Update () {
		// calculate the diference between the player's rotation and the balanceArrow rotation
		float difZRot = (playerHands.transform.rotation.z - balanceArrow.transform.rotation.z) * Mathf.Rad2Deg;
		balanceArrow.transform.RotateAround (balanceArrow.transform.position, Vector3.forward, difZRot);

		livesText.GetComponent<Text> ().text = "Lives: " + playerMotor.getCurrentLives() + "/" + playerMotor.getTotalLives();
		if (!playerMotor.getPlayerAlive()) {
			restartButton.SetActive(true);
		}
	}

	public void RestartLevel(){
		Application.LoadLevel ("office-nic");
	}
}
