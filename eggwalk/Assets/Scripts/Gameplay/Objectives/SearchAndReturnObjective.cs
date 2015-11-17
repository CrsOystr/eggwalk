using UnityEngine;
using System.Collections;

public class SearchAndReturnObjective : MonoBehaviour, Objective {

    [SerializeField] private int objectiveID;
    [SerializeField] private string objectiveName;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool hasCompleted;
    [SerializeField] private TriggerBox targetTrigger;

    private bool hasStarted;

    // Use this for initialization
    void Start ()
    {
        // Spawn Asset in world
        targetTrigger.TargetObject = targetObject;
    }

    public string getObjectiveName()
    {
        return objectiveName;
    }

    public int getObjectiveID()
    {
        return objectiveID;
    }

    public void startObjective()
    {
        hasStarted = true;
    }

    public void completeObjective()
    {
        hasCompleted = true;
    }

    public bool hasStartedObjective()
    {
        return hasStarted;
    }

    public bool hasCompletedObjective()
    {
        return hasCompleted;
    }
}
