using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    [SerializeField] private GameplayNotifier notifier;
    [SerializeField] private GameMode gameMode;
    [SerializeField] private List<GameObject> objectiveList;

    private int timeToStart;
    private float timeInLevel;
    private int score;
    private GameObject currentObjective;
    private List<string> deliveredItems;
    private bool isGameOver;
    private bool hasCompletedLevel;
    private bool hasStartedLevel;

	void Awake ()
    {
        if (notifier == null)
        {
            return;
        }

        Time.timeScale = 1.0f;
        this.score = 0;
        this.deliveredItems = new List<string>();

        this.timeToStart = gameMode.InitialTimeToStartLevel;
        this.hasStartedLevel = false;
        StartCoroutine(countdown());

        this.notifier.notify(new GameEvent(objectiveList, 
            GameEnumerations.EventCategory.Gameplay_InitializeEvents));
	}


    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (this.hasStartedLevel && !this.isGameOver)
        {
            this.timeInLevel += Time.deltaTime;
        }

		if(gameMode.CanPauseGame && Input.GetButtonDown("Pause") && !isGameOver)
        {
			Time.timeScale = (Time.timeScale != 0.0f) ? 0.0f : 1.0f;
            this.notifier.notify(new GameEvent(new List<GameObject> { },
                GameEnumerations.EventCategory.Gameplay_Paused));
            
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
                if (!gameMode.IsEndless)
                {
                    this.currentObjective = null;
                }

                Objective completedObj = objectiveList[i].GetComponent<Objective>();
                completedObj.completeObjective();
                return true;
            }
        }
        return false;
    }

    public bool restartEndlessObjective(GameObject player)
    {
        this.currentObjective = this.objectiveList[0];

        GameObject pickup = this.currentObjective.GetComponent<Objective>().getObjectiveItem();

        if (player != null)
        {
            notifier.notify(new GameEvent(new List<GameObject> { player, pickup },
                GameEnumerations.EventCategory.Player_StartedObjective));
            return true;
        } else
        {
            return false;
        }
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

    public Objective getObjectiveFromId(int id)
    {
        for (int i = 0; i < objectiveList.Count; i++)
        {
            Objective obj = this.objectiveList[i].GetComponent<Objective>();
            if (obj != null && obj.getObjectiveID() == id)
            {
                return obj;
            }
        }

        return null;
    }

    public void addToItemDeliveredList(string item)
    {
        this.deliveredItems.Add(item);
    }

    public string getLastDeliveredItem()
    {
        return (deliveredItems.Count > 0) ? deliveredItems[deliveredItems.Count - 1] : "Not Good";
    }

    public void initializeLevel()
    {
        if (gameMode.IsEndless)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            this.currentObjective = this.objectiveList[0];
            this.currentObjective.GetComponent<Objective>().initializeObjective();

            GameObject pickup = this.currentObjective.GetComponent<Objective>().getObjectiveItem();

            if (p != null)
            {
                p.GetComponent<PlayerMotor>().CanPlayerTurnAnywhere = gameMode.CanTurnAnywhere;
                notifier.notify(new GameEvent(new List<GameObject> { p, pickup }, 
                    GameEnumerations.EventCategory.Player_StartedObjective));
            }
        }
    }

    public void completeLevel()
    {
        this.notifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Gameplay_CompletedLevel));
    }

    private IEnumerator countdown()
    {
        yield return new WaitForSeconds(1.0f);
        if (timeToStart > -1)
        {
            TimeToStart--;
            notifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Gameplay_Countdown));
            if (timeToStart == 0)
            {
                notifier.notify(new GameEvent(null, GameEnumerations.EventCategory.Gameplay_StartLevel));
            }

            StartCoroutine(countdown());
        }
    }

    public int TimeToStart
    {
        get { return this.timeToStart; }
        private set { this.timeToStart = value; }
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().startPlayer(true);
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

    public void addToScore(int score)
    {
        this.score += score;
    }

    public GameMode getGameMode()
    {
        return this.gameMode;
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

	public int Score
	{
		get { return this.score; }
	}
}
