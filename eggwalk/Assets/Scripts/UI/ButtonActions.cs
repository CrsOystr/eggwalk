using UnityEngine;
using System.Collections;


// better way to handle button actions

public class ButtonActions : MonoBehaviour {

	public void loadScene(string scene) {
		Application.LoadLevel(scene);
	}

	public void exitGame() {
		Application.Quit ();
	}

}
