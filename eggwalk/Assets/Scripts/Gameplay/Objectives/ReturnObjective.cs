using UnityEngine;
using System.Collections.Generic;

public class ReturnObjective : MonoBehaviour, Objective
{

    [SerializeField] private string objectiveName;
    [SerializeField] private int objectiveId;
    [SerializeField] private TriggerBox triggerBox;
<<<<<<< HEAD
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private List<GameObject> items;
=======
<<<<<<< HEAD
<<<<<<< HEAD
=======
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private List<GameObject> items;
>>>>>>> parent of 0a2d922... attempting merge
=======
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private List<GameObject> items;
=======
>>>>>>> egg_collection_fix
>>>>>>> parent of 91f44dd... attempting cleanup
>>>>>>> master
    [SerializeField] private List<Transform> returnLocations;
    [SerializeField] private PlayerPrefsManager playerPrefsManager;

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
        int r = Random.Range(0, playerPrefsManager.AllEggsInGame.Count);
        int rl = Random.Range(0, returnLocations.Count);

<<<<<<< HEAD
        GameObject item = Instantiate(playerPrefsManager.LoadRandomEgg(), this.transform.position, this.transform.rotation) as GameObject;
=======
<<<<<<< HEAD
<<<<<<< HEAD
        GameObject item = Instantiate(playerPrefsManager.LoadRandomEgg(), this.transform.position, this.transform.rotation) as GameObject;
=======
        GameObject item = Instantiate(items[r], this.spawnLocation.position, this.spawnLocation.rotation) as GameObject;
>>>>>>> parent of 0a2d922... attempting merge
=======
        GameObject item = Instantiate(items[r], this.spawnLocation.position, this.spawnLocation.rotation) as GameObject;
=======
        GameObject item = Instantiate(playerPrefsManager.LoadRandomEgg(), this.transform.position, this.transform.rotation) as GameObject;
>>>>>>> egg_collection_fix
>>>>>>> parent of 91f44dd... attempting cleanup
>>>>>>> master
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
        playerPrefsManager.RecordSuccessfulDelivery(playerPrefsManager.LastEggInstantiated);
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
