using UnityEngine;
using System.Collections.Generic;

public class CollectingObjective : MonoBehaviour, Objective {

	[SerializeField] private string objectiveName;
	[SerializeField] private int objectiveID;
	[SerializeField] private List<GameObject> items;
	[SerializeField] private bool hasCompleted;
	[SerializeField] private TriggerBox targetTrigger;
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private List<Transform> potentialSpawnPoints;

    private GameObject currentItem;
    private int score;
    private bool hasStarted;
	
	void Start () 
	{
        List<int> points = new List<int>();

        while (points.Count < 5)
        {
            int r = Random.Range(0, potentialSpawnPoints.Count - 1);
            if (!points.Contains(r))
            {
                points.Add(r);
            }
        }

        for (int i = 0; i < points.Count; i++)
        {
            float rf = Random.Range(0.0f, 1.0f);
            int r = 0;

            if (rf < 0.20f)
            {
                r = 1;
            } else if (rf >= 0.20f && rf < 0.50f)
            {
                r = 2;
            }

            Transform spawnPoint = potentialSpawnPoints[points[i]];
            GameObject current = Instantiate(items[r], 
                spawnPoint.position, spawnPoint.rotation) as GameObject;
            this.currentItem = current;
            targetTrigger.TargetObjects.Add(current);
        }

		particles.enableEmission = false;
	}

	public string getObjectiveName()
	{
		return this.objectiveName;
	}

	public int getObjectiveID() 
	{
		return this.objectiveID;
	}

    public GameEnumerations.ObjectiveType getObjectiveType()
    {
        return GameEnumerations.ObjectiveType.Objective_Collect;
    }

    public GameObject getObjectiveItem()
    {
        return this.currentItem;
    }

    public Transform getObjectiveDestination()
	{
		return this.targetTrigger.transform;
	}

    public void initializeObjective()
    {

    }

    public void startObjective()
	{
		this.hasStarted = true;
		this.particles.enableEmission = true;
	}

	public void completeObjective()
	{
        int r = Random.Range(0, items.Count);
        Transform spawnPoint = potentialSpawnPoints [Random.Range (0, potentialSpawnPoints.Count - 1)];
		GameObject current =  Instantiate(items[r], spawnPoint.position, spawnPoint.rotation) as GameObject;
		targetTrigger.addTargetObject(current);
	}

	public bool hasStartedObjective()
	{
		return this.hasStarted;
	}

	public bool hasCompletedObjective()
	{
		return this.hasCompleted;
	}
}
