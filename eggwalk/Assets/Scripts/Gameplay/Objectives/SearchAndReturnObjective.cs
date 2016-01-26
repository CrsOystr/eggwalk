using UnityEngine;
using System.Collections.Generic;

public class SearchAndReturnObjective : MonoBehaviour, Objective {

    [SerializeField] private int objectiveID;
    [SerializeField] private string objectiveName;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool hasCompleted;
    [SerializeField] private TriggerBox targetTrigger;
    [SerializeField] private ParticleSystem particles;

    private bool hasStarted;

    // Use this for initialization
    void Start ()
    {
        targetTrigger.addTargetObject(targetObject);
        particles.enableEmission = false;
    }

    public string getObjectiveName()
    {
        return objectiveName;
    }

    public int getObjectiveID()
    {
        return objectiveID;
    }

    public GameEnumerations.ObjectiveType getObjectiveType()
    {
        return GameEnumerations.ObjectiveType.Objective_Search_Return;
    }

    public GameObject getObjectiveItem()
    {
        return this.targetObject;
    }

    public Transform getObjectiveDestination()
    {
        return targetTrigger.Destination;
    }

    public void initializeObjective()
    {
    }

    public void startObjective()
    {
        hasStarted = true;
        particles.enableEmission = true;
    }

    public void completeObjective()
    {
        hasCompleted = true;
        particles.enableEmission = false;
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
