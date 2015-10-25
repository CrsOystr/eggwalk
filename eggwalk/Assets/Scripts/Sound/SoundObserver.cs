using UnityEngine;
using System.Collections;

public class SoundObserver : MonoBehaviour, Observer {

	public void onNotify(GameEvent e) {
		GameEnumerations.EventCategory category = e.Category;
		
		switch (category) {
			case GameEnumerations.EventCategory.Player_IsHurt:
			{
				Debug.Log("From the SoundObserver");
				break;
			}
			case GameEnumerations.EventCategory.Player_IsDead:
			{
				Debug.Log("From the SoundObserver");
				break;
			}
		}
	}
}
