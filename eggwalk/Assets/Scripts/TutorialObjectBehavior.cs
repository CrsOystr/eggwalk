using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Image))]

public class TutorialObjectBehavior : MonoBehaviour
{
	[SerializeField] private GameState gs;
	[SerializeField] private GameObject disp;
	public bool pause;
	public bool killOnExit;
	public bool endTutorial;


	//Image img;

	// Use this for initialization
	void Start ()
	{
		//zone = GetComponent<BoxCollider> ();
		//disp = GetComponent<GameObject> ();
	}
	
	// Update is called once per frame
	/*
	void Update ()
	{
		
	}

	public bool isOn;
	
	public void setOn(bool o)
	{
		isOn = o;
	}
	
	public bool isSwitchOn()
	{
		return isOn;
	}
	*/

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.tag == "Player") {
			disp.SetActive(true);
			if (pause)
				gs.tutorialPause();
			/*
			if (pause)
				PauseGame();
			else
				DisplayContinuously();
				*/
			//display the UI image
		}
			//sb.setOn (true);
	}
	
	void OnTriggerExit(Collider col) {
		
		if (col.gameObject.tag == "Player") {
			disp.SetActive(false);
			if (!pause)
				gs.tutorialPause();
			if (killOnExit)
				this.gameObject.SetActive(false);
		}
			//sb.setOn (false);
	}
	/*
	void OnTriggerStay(Collider col)
	{

		if (pause)
			PauseGame ();
	}

	private void PauseGame()
	{
		
	}

	private void DisplayContinuously()
	{

	}
	*/
}

