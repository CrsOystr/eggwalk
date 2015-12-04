using UnityEngine;
using System.Collections;

public class SoundSystem : MonoBehaviour {
	
	/* 	ADD REFERENCES TO NEW SOUNDS YOU WILL WANT TO USE HERE:
	*	use the format below but use a descriptive name in palace of "exampleAudio"
	*	you will then have to drag the actual audio source from the object it is on to this script.
	*	This script can be found under GameplaySystem/PlayerNotifier/SoundObserver in the scene.
	*	Contact Nic with questions/concerns
	 */

	[SerializeField] private AudioSource exampleAudio;
	[SerializeField] private AudioSource missionMusic;
	[SerializeField] private AudioSource ambientNatureSounds;
	//	[SerializeField] private AudioSource ambientCitySounds;



	//example of how to do multiple items I.E. all firetrucks
	[SerializeField] private AudioSource[] exampleAudioList;



	/*FUNCTIONS TO USE THE ABOVE AUDIOSOURCE:
	 * use descriptive function names, these will be accessesed through the SOUNDOBSERVER script.
	 * Seperate functions out as much as possible, I.E. make a "turn on all firetrucks" and "turn on waterfountain sound"
	 * as apposed to "put out the fire sounds"
	 */
	public void playExampleAudio()
	{
		exampleAudio.Play ();
		//exampleAudio.enabled (false);
		//exampleAudio.Stop ();
	}

	public void playExampleAudioList()
	{
		foreach (AudioSource source in exampleAudioList)
			exampleAudio.Play ();
		//exampleAudio.enabled (false);
		//exampleAudio.Stop ();
	}
	public void playMissionMusicAudio()
	{
		if (!missionMusic.isPlaying) {
			missionMusic.Play ();
			ambientNatureSounds.volume = 0.63f;
		}
	}
}
