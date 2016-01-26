using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (BoxCollider))]

public class TriggerBox : MonoBehaviour {
    private List<GameObject> targetObjects;
    private bool hasActivated = false;

    void Awake()
    {
        this.targetObjects = new List<GameObject>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    void Update()
    {
        if (this.targetObjects == null)
        {
            Debug.Log("Hello");
        }
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
        return targetObjects.Contains(target);
    }

    public void addTargetObject(GameObject target)
    {
        if (this.targetObjects == null)
        {
            this.targetObjects = new List<GameObject>();
        }

        this.targetObjects.Add(target);
    }

    public bool Activated
    {
        get { return hasActivated; }
        set { hasActivated = value; }
    }
    
    public List<GameObject> TargetObjects
    {
        get { return targetObjects; }
        set { targetObjects = value; }
    }

    public Transform Destination
    {
        get { return this.transform; }
    }
}
