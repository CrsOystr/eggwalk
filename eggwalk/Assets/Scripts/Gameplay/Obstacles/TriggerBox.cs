using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class TriggerBox : MonoBehaviour {
    private GameObject targetObject;
    private bool hasActivated = false;

    void Start()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider col)
    { 
        if (!hasActivated && col.GetComponent<PlayerMotor>() != null)
        {
            hasActivated = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (hasActivated && col.GetComponent<PlayerMotor>() != null)
        {
            hasActivated = false;
        }
    }

    public bool isTargetEqual(GameObject target)
    {
        return ReferenceEquals(this.targetObject, target);
    }

    public bool Activated
    {
        get { return hasActivated; }
        set { hasActivated = value; }
    }

    public GameObject TargetObject
    {
        get { return targetObject; }
        set { targetObject = value; }
    }
}
