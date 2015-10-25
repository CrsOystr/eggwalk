using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : MonoBehaviour {
	[SerializeField] private Text LifeText;
	[SerializeField] private GameObject RestartButton;

	public void setCurrentLives(int currentLives, int maxLives) {
		LifeText.text = "Lives " + currentLives + "/" + maxLives;
	}

	public void setVisibilityToRestart(bool isVisible) {
		RestartButton.SetActive (true);
	}

	public void returnButtonAction() {
		Debug.Log ("Quit Here");
	}
}
