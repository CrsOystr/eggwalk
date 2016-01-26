using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GradientTest : MonoBehaviour {

    public Image img;
    public Gradient gradient;
    public float strobeDuration = 2.0f;
	
	// Update is called once per frame
	void Update () {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1.0f);
        img.color = gradient.Evaluate(t);
	}
}
