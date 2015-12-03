using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScorboardBehavior : MonoBehaviour {

	public string levelName;
	public Text[] scoreTexts;

	// Use this for initialization
	void Start () {


		for (int i = 0; i < scoreTexts.Length; i++) {
			// all scores/times are saved in the format "Score_1", "Score_7", "Score_10", etc.
			if(PlayerPrefs.HasKey(levelName + "_score_" + (i+1).ToString())) {

			}
		}
	}

	public void loadScores() {
		
	}

}
