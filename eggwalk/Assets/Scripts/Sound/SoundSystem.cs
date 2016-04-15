using UnityEngine;
using System.Collections;

public class SoundSystem : MonoBehaviour {
	
	/* 	ADD REFERENCES TO NEW SOUNDS YOU WILL WANT TO USE HERE:
	*	use the format below but use a descriptive name in palace of "exampleAudio"
	*	you will then have to drag the actual audio source from the object it is on to this script.
	*	This script can be found under GameplaySystem/PlayerNotifier/SoundObserver in the scene.
	*	Contact Nic with questions/concerns
	 */

	[SerializeField] private AudioSource missionMusic;
	[SerializeField] private AudioSource missionMusicLoop;
	[SerializeField] private AudioSource missionMusicEnd;
	[SerializeField] private AudioSource deathSound;
	[SerializeField] private AudioSource ambientNatureSounds;
	[SerializeField] private AudioSource completedObjectiveAudio;
	[SerializeField] private AudioSource eggDropSound;
	[SerializeField] private AudioSource eggCrackSound;
	[SerializeField] private AudioSource childrenLaughingSound;
	[SerializeField] private AudioSource carHonkSound;


	//	[SerializeField] private AudioSource ambientCitySounds;



	//example of how to do multiple items I.E. all firetrucks
	[SerializeField] public AudioSource[] playerHurtSounds;
	[SerializeField] public AudioSource[] playerCrySounds;




	/*FUNCTIONS TO USE THE ABOVE AUDIOSOURCE:
	 * use descriptive function names, these will be accessesed through the SOUNDOBSERVER script.
	 * Seperate functions out as much as possible, I.E. make a "turn on all firetrucks" and "turn on waterfountain sound"
	 * as apposed to "put out the fire sounds"
	 */
	bool dead = false;
	int lastHurtSoundPlayed = 0;
	int lastCrySoundPlayed = 0;

	public void playPlayerHitSound()
	{
		if (dead == false) {
			eggCrackSound.Play ();
			int randy = Random.Range (0, playerHurtSounds.Length);
			while (randy == lastHurtSoundPlayed)
				randy = Random.Range (0, playerHurtSounds.Length);
			for (int i = 0; i < playerHurtSounds.Length; i++) {
				if (i == randy)
					playerHurtSounds [i].Play ();
			}
			lastHurtSoundPlayed = randy;
		} else {
			if (childrenLaughingSound.isPlaying == false)
				childrenLaughingSound.Play ();
			int randy = Random.Range (0, playerCrySounds.Length);
			while (randy == lastCrySoundPlayed)
				randy = Random.Range (0, playerCrySounds.Length);
			for (int i = 0; i < playerCrySounds.Length; i++) {
				if (i == randy)
					playerCrySounds [i].Play ();
			}
			lastCrySoundPlayed = randy;
		
		}
	}

	public void playCompletedObjectiveAudio()
	{
		completedObjectiveAudio.Play ();
		eggDropSound.Play ();
	}

	public void playCarBehindAudio()
	{
		if (carHonkSound.isPlaying == false)
			carHonkSound.Play ();
	}

	public void playDeathCryAudio()
	{
		dead = true;
		for (int i = 0; i < playerHurtSounds.Length; i++) {
			playerHurtSounds [i].Stop ();
		}
		deathSound.Play ();
	}

	public IEnumerator playMissionMusicAudio()
	{
		//bool go = true;

		missionMusic.Play ();
		yield return new WaitForSeconds (missionMusic.clip.length); //return new WaitForSeconds(missionMusic.length);
		missionMusicLoop.Play ();

		/*
		if (!dead) {
			missionMusicLoop.Play ();
			yield return new WaitForSeconds (missionMusicLoop.clip.length);
		}
		else {
			missionMusicEnd.Play ();
			go = false;
		}

		while (go) {
			if (!dead) {
				missionMusicLoop.Play ();
				yield return new WaitForSeconds (missionMusicLoop.clip.length);
			}
			else {
				missionMusicEnd.Play ();
				go = false;
			}
		}*/
	}
}