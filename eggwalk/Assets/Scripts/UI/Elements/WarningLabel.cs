using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningLabel : MonoBehaviour {

    [SerializeField] private Gradient gradient;
    [SerializeField] private Image exclamation;
    [SerializeField] private float flashingFrequency;

    private bool active;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float t = Mathf.PingPong(Time.time / flashingFrequency, 1.0f);
        this.exclamation.color = gradient.Evaluate(t);
    }

    public bool Active
    {
        get { return this.active; }
        set { this.active = value; }
    }
}
