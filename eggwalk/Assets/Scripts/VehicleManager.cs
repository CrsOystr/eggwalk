using UnityEngine;
using System.Collections.Generic;

public class VehicleManager : MonoBehaviour {

	[SerializeField] private int _numberOfVehicles;
	[SerializeField] private float _secondsToRespawn;
	[SerializeField] private GameObject[] _vehiclesToSpawn;
	
	private List<VehicleBehavior> _vehicleBehaviors;
	private GameObject[] _spawnPoints;
	private float[] _respawnCounters;
    private Vector3[] _lastPositions;

    public GameObject[] SpawnPoints
    {
        get { return _spawnPoints; }
    }

	// Use this for initialization
	void Start () {
		
		_spawnPoints = GameObject.FindGameObjectsWithTag("Intersection Point");

        Debug.Log(_spawnPoints.Length);

		// shuffle spawn points
		for(int i = 0; i < _spawnPoints.Length; i++) {
			int r = Random.Range(0, _spawnPoints.Length);
			GameObject temp = _spawnPoints[i];
			_spawnPoints[i] = _spawnPoints[r];
			_spawnPoints[r] = temp;
        }

        Debug.Log("spawn points have been shuffled");

        _vehicleBehaviors = new List<VehicleBehavior>();
		
        // can't have more vehicles than intersection points
		if(_numberOfVehicles > _spawnPoints.Length/4) _numberOfVehicles = _spawnPoints.Length/4;
		
		_respawnCounters = new float[_numberOfVehicles];
		_lastPositions = new Vector3[_numberOfVehicles];



        Debug.Log("attempting to spawn vehicles...");

        for (int i = 0; i < _numberOfVehicles; i++) {
			spawnVehicle(i);

            Debug.Log("spawned vehicle #" + i);
        }

	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < _vehicleBehaviors.Count; i++) {
			_respawnCounters[i] += Time.deltaTime;
			
			if(_respawnCounters[i] > _secondsToRespawn && 
				Vector3.Distance(_vehicleBehaviors[i].transform.position, _lastPositions[i]) < 1f) {
			
				if(!isInsideCameraFrustrum(_vehicleBehaviors[i].gameObject.GetComponent<Collider>())) {
					Destroy(_vehicleBehaviors[i].gameObject);
					spawnVehicle(i);
					Debug.Log("respawn!");
				}
			
			} else {
				_respawnCounters[i] -= _secondsToRespawn;
			}
			
			_lastPositions[i] = new Vector3(_vehicleBehaviors[i].transform.position.x,
				_vehicleBehaviors[i].transform.position.y,
				_vehicleBehaviors[i].transform.position.z);
		}
	}
	
	private void spawnVehicle(int index) {
		
		int r1 = Random.Range(0, _vehiclesToSpawn.Length);
        VehicleBehavior vehicle = Instantiate(_vehiclesToSpawn[r1]).GetComponent<VehicleBehavior>();
        _vehicleBehaviors.Add(vehicle);

        float initY = vehicle.transform.position.y;
        vehicle.transform.position = new Vector3(
            _spawnPoints[index].transform.position.x,
            initY,
            _spawnPoints[index].transform.position.z);

        vehicle.Target = _spawnPoints[index].GetComponentInParent<IntersectionBehavior>().transform;
        vehicle.transform.SetParent(transform.parent);
        vehicle.VehicleManager = this;
	
	}

    private bool isInsideCameraFrustrum(Collider col)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }


}
