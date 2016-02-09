using UnityEngine;
using System.Collections.Generic;

public class ReturnObjective : MonoBehaviour, Objective {

    [SerializeField] private string objectiveName;
    [SerializeField] private int objectiveId;
    [SerializeField] private TriggerBox triggerBox;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private List<GameObject> items;
    [SerializeField] private List<Transform> returnLocations;

    private GameObject currentItem;
    private bool hasStarted;
    private bool hasCompleted;

    public string getObjectiveName()
    {
        return this.objectiveName;
    }

    public int getObjectiveID()
    {
        return this.objectiveId;
    }

    public GameEnumerations.ObjectiveType getObjectiveType()
    {
        return GameEnumerations.ObjectiveType.Objective_Return;
    }

    public Transform getObjectiveDestination()
    {
        return this.triggerBox.transform;
    }

    public GameObject getObjectiveItem()
    {
        return this.currentItem;
    }

    public void initializeObjective()
    {
        int r = Random.Range(0, items.Count);
        int rl = Random.Range(0, returnLocations.Count);

        GameObject item = Instantiate(items[r], this.spawnLocation.position, this.spawnLocation.rotation) as GameObject;
        this.currentItem = item;

        this.triggerBox.transform.position = returnLocations[rl].position;
        this.triggerBox.addTargetObject(item);
    }

    public void startObjective()
    {
        this.hasStarted = true;
    }

    public void completeObjective()
    {
        initializeObjective();
    }

    public bool hasStartedObjective()
    {
        return this.hasStarted;
    }

    public bool hasCompletedObjective()
    {
        return false;
    }
}
