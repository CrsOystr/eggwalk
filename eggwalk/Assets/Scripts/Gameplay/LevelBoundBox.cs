using UnityEngine;
using System.Collections;

public class LevelBoundBox : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerMotor>() != null)
        {
            col.GetComponent<PlayerMotor>().warpToSafePoint();
        }
    }
}
