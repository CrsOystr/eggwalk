using UnityEngine;
using System.Collections;

public class ChildCollisionSignal : MonoBehaviour {

    [SerializeField] private PlayerMotor player;

    void OnTriggerEnter(Collider col)
    {
        //print("Trigger");
        //player.signalCollisionEnter(col.co);
    }
}
