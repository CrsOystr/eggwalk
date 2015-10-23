using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwitchBehavior))]
[RequireComponent(typeof(BoxCollider))]

public class DetectPlayerTriggerBehavior : MonoBehaviour {

	private SwitchBehavior sb;

	void Start() {
		sb = GetComponent<SwitchBehavior> ();
	}

	void OnTriggerEnter(Collider col) {

		if (col.gameObject.tag == "Player")
			sb.setOn (true);
	}
	
	void OnTriggerExit(Collider col) {
		
		if (col.gameObject.tag == "Player")
			sb.setOn (false);
		
	}

}
