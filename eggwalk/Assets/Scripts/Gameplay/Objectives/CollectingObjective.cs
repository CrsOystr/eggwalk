using UnityEngine;
using System.Collections.Generic;

public class CollectingObjective : MonoBehaviour, Objective {

	[SerializeField] private string objectiveName;
	[SerializeField] private int objectiveID;
	[SerializeField] private GameObject item;
	[SerializeField] private bool hasCompleted;
	[SerializeField] private TriggerBox targetTrigger;
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private List<Transform> potentialSpawnPoints;

	private bool hasStarted;
	
	void Start () 
	{
		Transform spawnPoint = potentialSpawnPoints [Random.Range (0, potentialSpawnPoints.Count - 1)];
		GameObject current =  Instantiate(item, spawnPoint.position, spawnPoint.rotation) as GameObject;
		targetTrigger.TargetObject = current;
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

	public Transform getObjectiveDestination()
	{
		return this.targetTrigger.transform;
	}

	public void startObjective()
	{
		this.hasStarted = true;
		this.particles.enableEmission = true;
	}

	public void completeObjective()
	{
		Transform spawnPoint = potentialSpawnPoints [Random.Range (0, potentialSpawnPoints.Count - 1)];
		GameObject current =  Instantiate(item, spawnPoint.position, spawnPoint.rotation) as GameObject;
		targetTrigger.TargetObject = current;
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
