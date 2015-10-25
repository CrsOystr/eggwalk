using UnityEngine;
using System.Collections;

public class GameplayObserver : MonoBehaviour, Observer {

	public void onNotify(GameEvent e) {
		GameEnumerations.EventCategory category = e.Category;
		PlayerCharacter playerCharacter = e.Entity.GetComponent<PlayerCharacter> ();
		switch (category) {
			case GameEnumerations.EventCategory.Player_IsHurt:
			{
				playerCharacter.RecieveDamage(1);
				break;
			}
			case GameEnumerations.EventCategory.Player_IsDead:
			{
				Debug.Log("From the GameplayObserver");
				break;
			}
		}
	}
}