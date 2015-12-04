using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    [SerializeField] private GameplayNotifier notifier;
    [SerializeField] private List<GameObject> objectiveList;
    [SerializeField] private int initialTimeToStart = 3;
    private int countdownTime;
    private GameObject currentObjective;
    private List<string> deliveredItems;
    private bool isGameOver;
    private bool hasCompletedLevel;
    private bool hasStartedLevel;
    private float timeInLevel;

	// Use this for initialization
	void Start () {
	    if (notifier != null)
        {
            this.countdownTime = initialTimeToStart;
            StartCoroutine(countdown());
            this.notifier.notify(new GameEvent(objectiveList, GameEnumerations.EventCategory.Gameplay_InitializeEvents));
            this.deliveredItems = new List<string>();
            this.hasStartedLevel = false;
        }
	}

    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }


        if (this.HasStartedLevel)
        {
            timeInLevel += Time.deltaTime;
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

    public void completeLevel()
    {
        this.notifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Gameplay_CompletedLevel));
    }

    private IEnumerator countdown()
    {
        yield return new WaitForSeconds(1.0f);
        if (countdownTime > -1)
        {
            CountdownTime--;
            notifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Gameplay_Countdown));
            if (countdownTime == 0)
            {
                notifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Gameplay_StartLevel));
            }
            StartCoroutine(countdown());
        }
    }

    public int CountdownTime
    {
        get { return this.countdownTime; }
        private set { this.countdownTime = value; }
    }

    public int InitialTimeToStart
    {
        get { return this.initialTimeToStart; }
        private set { this.initialTimeToStart = value; }
    }

    public bool HasCompletedLevel
    {
        get
        {
            for (int i = 0; i < objectiveList.Count; i++)
            {
                Objective obj = objectiveList[i].GetComponent<Objective>();
                if (!obj.hasCompletedObjective())
                {
                    return false;
                }
            }

            return true;
        }
    }

    public bool IsGameOver
    {
        get { return this.isGameOver; }
        set { this.isGameOver = value; }
    }

    public void startGame()
    {
        GameObject.FindObjectOfType<PlayerMotor>().startPlayer(true);
        this.HasStartedLevel = true;
    }

    public Transform getDestinationFromObjective(int id)
    {
        for (int i = 0; i < objectiveList.Count; i++)
        {
            Objective obj = objectiveList[i].GetComponent<Objective>();
            if (obj.getObjectiveID() == id)
            {
                return obj.getObjectiveDestination();
            }
        }

        return null;
    }

    public bool HasStartedLevel
    {
        get { return this.hasStartedLevel; }
        set { this.hasStartedLevel = value; }
    }

    public float TimeInLevel
    {
        get { return this.timeInLevel; }
    }
}
