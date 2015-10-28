using UnityEngine;
using System.Collections;

public class HUDBehavior : MonoBehaviour {

	public GameObject player;
	public GameObject balanceMeter;
	public GameObject balanceArrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float difZRot = (player.transform.rotation.z - balanceArrow.transform.rotation.z) * Mathf.Rad2Deg;
		balanceArrow.transform.RotateAround (balanceArrow.transform.position, Vector3.forward, difZRot);
	}
}
