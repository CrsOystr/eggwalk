using UnityEngine;
using System.Collections;

public class SoundObserver : MonoBehaviour, Observer {

	[SerializeField] private SoundSystem SoundSys;

	public void onNotify(GameEvent e) {
		GameEnumerations.EventCategory category = e.Category;
		
		switch (category) {
			case GameEnumerations.EventCategory.Player_IsDead:
			{
				
				SoundSys.playDeathCryAudio();
				break;
			}
			case GameEnumerations.EventCategory.Player_IsHurt:
			{
				SoundSys.playPlayerHurtSound();
				break;
			}
			case GameEnumerations.EventCategory.Player_ReturnedTarget:
			{
				SoundSys.playCompletedObjectiveAudio();
				break;
			}
			case GameEnumerations.EventCategory.Player_StartedObjective:
			{
				SoundSys.playMissionMusicAudio();
				break;
			}
		}
	}
}
