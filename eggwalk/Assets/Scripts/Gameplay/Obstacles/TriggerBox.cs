using UnityEngine;
using System.Collections;

public class TriggerBox : MonoBehaviour {
    public GameObject TargetObstacle;
    private bool HasActivated = false;
	
    void OnTriggerEnter(Collider col)
    { 
        if (!HasActivated)
        {
            TargetObstacle.gameObject.GetComponent<Obstacle>().activate();
            HasActivated = true;
        }

    }
}
