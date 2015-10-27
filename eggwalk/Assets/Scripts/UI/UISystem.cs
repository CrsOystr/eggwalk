using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : MonoBehaviour {
	[SerializeField] private Text LifeText;
	[SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject BalanceTriangle;

	public void setCurrentLives(int currentLives, int maxLives) {
		LifeText.text = "Lives " + currentLives + "/" + maxLives;
	}

	public void setVisibilityToRestart(bool isVisible) {
		RestartButton.SetActive (true);
	}

	public void returnButtonAction(string level) {
        Application.LoadLevel(level);
	}

    public void setRotationToBalanceBar(float deltaRotation)
    {
        BalanceTriangle.transform.localEulerAngles = Vector3.forward * deltaRotation * -1.0f;
    }
}
