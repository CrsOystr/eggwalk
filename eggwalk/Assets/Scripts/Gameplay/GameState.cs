using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    [SerializeField] private GameplayNotifier notifier;
    [SerializeField] private GameObject currentObjective;
    [SerializeField] private List<GameObject> objectiveList;
    private bool isGameOver;

	// Use this for initialization
	void Start () {
	    if (notifier != null)
        {
            notifier.notify(new GameEvent(objectiveList, GameEnumerations.EventCategory.Gameplay_InitializeEvents));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private Objective getObjectiveFromGameObject(GameObject gameObject)
    {
        return gameObject.GetComponent<Objective>();
    }
}
