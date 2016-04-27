using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Image))]

public class TutorialObjectBehavior : MonoBehaviour
{
	[SerializeField] private GameState gs;
	[SerializeField] private UISystem UISys;
	[SerializeField] private GameObject disp;
	[SerializeField] private GameObject but;
	[SerializeField] private EventSystem es;


	public bool pause;
	public bool killOnExit;
	public bool endTutorial;



	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.tag == "Player") {
			if (endTutorial == false)
				disp.SetActive (true);
			else
				StartCoroutine (endTutorialProcess ());

			if (pause) {
				gs.tutorialPause ();
				es.SetSelectedGameObject (but);
			}	
		}
	}
	
	void OnTriggerExit(Collider col) {
		
		if (col.gameObject.tag == "Player") {
			if (!endTutorial)
				disp.SetActive(false);
			if (killOnExit)
				this.gameObject.SetActive(false);
				
		}
	}

	private IEnumerator endTutorialProcess()
	{
		//Text tx = disp.GetComponent<Text>();
		yield return new WaitForSeconds(1.5f);
		gs.tutorialPause();
		disp.SetActive (true);
		es.SetSelectedGameObject (but);
		//tx.text = "Tutorial Completed!";
		//yield return new WaitForSeconds (2f);

	}
}

