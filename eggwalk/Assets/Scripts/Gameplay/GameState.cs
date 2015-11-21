using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    [SerializeField] private GameplayNotifier notifier;
    [SerializeField] private List<GameObject> objectiveList;
    private GameObject currentObjective;
    private List<string> deliveredItems;
    private bool isGameOver;

	// Use this for initialization
	void Start () {
	    if (notifier != null)
        {
            this.notifier.notify(new GameEvent(objectiveList, GameEnumerations.EventCategory.Gameplay_InitializeEvents));
            this.deliveredItems = new List<string>();
        }
	}

    public bool startObjective(int id)
    {
        for (int i = 0; i < objectiveList.Count; i++)
        {
            Objective obj = objectiveList[i].GetComponent<Objective>();
            if (id == obj.getObjectiveID())
            {
                this.currentObjective = objectiveList[i];
                objectiveList[i].GetComponent<Objective>().startObjective();
                return true;
            }
        }

        return false;
    }

    public bool completeObjective(int id)
    {
        for (int i = 0; i < objectiveList.Count; i++)
        {
            Objective obj = objectiveList[i].GetComponent<Objective>();
            if (id == obj.getObjectiveID())
            {
                this.currentObjective = null;
                objectiveList[i].GetComponent<Objective>().completeObjective();
                return true;
            }
        }
        return false;
    }

    public GameObject getCurrentObjective()
    {
        return this.currentObjective;
    }

    public List<Objective> getObjectiveList()
    {
        List<Objective> objectives = new List<Objective>();
        for (int i = 0; i < this.objectiveList.Count; i++)
        {
            Objective obj = objectiveList[i].GetComponent<Objective>();
            if (!obj.hasCompletedObjective())
            {
                objectives.Add(objectiveList[i].GetComponent<Objective>());
            }
        }

        return objectives;
    }

    private Objective getObjectiveFromGameObject(GameObject gameObject)
    {
        return gameObject.GetComponent<Objective>();
    }

    public void addToItemDeliveredList(string item)
    {
        this.deliveredItems.Add(item);
    }

    public string getLastDeliveredItem()
    {
        return (deliveredItems.Count > 0) ? deliveredItems[deliveredItems.Count - 1] : "Not Good";
    }
}
