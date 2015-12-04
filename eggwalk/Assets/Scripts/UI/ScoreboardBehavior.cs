using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreboardBehavior : MonoBehaviour {

	public string levelName;
	public Text[] scoreTexts;

	private PlayerPrefsManager ppm = new PlayerPrefsManager();

	void Start() {
		loadScores ();
	}

	private void loadScores() {
		float[] scores = ppm.getTimeScores (levelName, 10);

		for(int i = 0; i < scoreTexts.Length; i++) {
			scoreTexts[i].text = scores[i].ToString();
		}
	}
	
}
