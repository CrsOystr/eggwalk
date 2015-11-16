using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class SearchAndReturnObjective : MonoBehaviour, Objective {

    [SerializeField] private string objectiveName;
    [SerializeField] private GameObject item;
    [SerializeField] private Transform assetStartLocation;
    [SerializeField] private bool hasCompleted;
    private BoxCollider boxCollider;

    // Use this for initialization
    void Start ()
    {
        this.boxCollider = GetComponent<BoxCollider>();
        this.boxCollider.isTrigger = true;

        // Spawn Asset in world
        Instantiate(item, assetStartLocation.position, assetStartLocation.rotation);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PlayerMotor>() != null)
        {
            this.hasCompleted = true;
        }
    }

    public string getObjectiveName()
    {
        return objectiveName;
    }

    public bool hasCompletedObjective()
    {
        return hasCompleted;
    }
}
