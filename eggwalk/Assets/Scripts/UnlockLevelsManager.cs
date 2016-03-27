using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnlockLevelsManager : MonoBehaviour {

	[SerializeField] private PlayerPrefsManager _ppm;
	[SerializeField] private List<SelectLevelButton> _levelButtons;
	[SerializeField] private List<UnlockLevelData> _unlockLevelData;

	void OnEnable() 
	{
		int totalEggsDelivered = _ppm.GetTotalEggsDelivered ();
		for (int i = 0; i < _levelButtons.Count; i++) {
			Button button = _levelButtons [i].GetComponent<Button> ();
			Text buttonText = _levelButtons [i].GetComponentInChildren<Text> ();
			if (_levelButtons [i].LevelName == _unlockLevelData [i].levelName && totalEggsDelivered >= _unlockLevelData [i].eggsToUnlock) {
				button.enabled = true;
				buttonText.text = (i + 1).ToString();
			} else {
				button.enabled = false;
				buttonText.text = "Locked!";
			}
		}
	}

	[System.Serializable]
	class UnlockLevelData
	{
		public string levelName;
		public int eggsToUnlock;
	}

}
