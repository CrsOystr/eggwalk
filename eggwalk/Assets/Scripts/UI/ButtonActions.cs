using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


// better way to handle button actions

public class ButtonActions : MonoBehaviour {

	public void loadScene(string scene) {
        SceneManager.LoadScene(scene);
	}

	public void exitGame() {
		Application.Quit ();
	}

}
