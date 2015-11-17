using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    [SerializeField] private GameplayNotifier notifier;
    [SerializeField] private List<GameObject> objectiveList;
    private GameObject currentObjective;
    private bool isGameOver;

	// Use this for initialization
	void Start () {
	    if (notifier != null)
        {
            notifier.notify(new GameEvent(objectiveList, GameEnumerations.EventCategory.Gameplay_InitializeEvents));
        }
	}
	

    public bool startObjective(int id)
    {
        for (int i = 0; i < objectiveList.Count; i++)
        {
            Objective obj = objectiveList[i].GetComponent<Objective>();
            if (id == obj.getObjectiveID())
            {
                currentObjective = objectiveList[i];
                objectiveList[i].GetComponent<Objective>().startObjective();
                return true;
            }
        }

        return false;
    }

    private Objective getObjectiveFromGameObject(GameObject gameObject)
    {
        return gameObject.GetComponent<Objective>();
    }
}
