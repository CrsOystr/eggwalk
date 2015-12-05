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
			if(scores[i] != 0f) {
				int mins = (int) (scores[i] / 60f);
				int seconds = (int) (scores[i] % 60f);
				int miliseconds = int.Parse(scores[i].ToString().Split('.')[1].Substring(0, 2));
				
				scoreTexts[i].text = (i+1).ToString() + "   " + mins.ToString() + ":" + seconds.ToString() + ":" + miliseconds.ToString();
			} else {
				scoreTexts[i].text = (i+1).ToString() + "   --:--:--";
			}
		}
	}
	
}
