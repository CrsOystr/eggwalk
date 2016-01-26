using UnityEngine;
using System.Collections;

public class VehicleManager : MonoBehaviour {

    [SerializeField] private VehicleBehavior[] _vehicleBehaviors;
    [SerializeField] private GameObject[] _vehicles;
    [SerializeField] private float _secondsToRespawn;

    private Transform[] _initTargets;
    private Vector3[] _initPositions;
    private Quaternion[] _initRotations;
    private VehicleBehavior.DriveDirection[] _initDriveDirections;
    private float[] _respawnCounters;
    private Vector3[] _previousPositions;

	// Use this for initialization
	void Start () {

        _initTargets = new Transform[_vehicleBehaviors.Length];
        _initPositions = new Vector3[_vehicleBehaviors.Length];
        _initRotations = new Quaternion[_vehicleBehaviors.Length];
        _initDriveDirections = new VehicleBehavior.DriveDirection[_vehicleBehaviors.Length];
        _respawnCounters = new float[_vehicleBehaviors.Length];
        _previousPositions = new Vector3[_vehicleBehaviors.Length];

        for (int i = 0; i < _vehicleBehaviors.Length; i++)
        {
            _initTargets[i] = _vehicleBehaviors[i].target;
            _initPositions[i] = new Vector3(_vehicleBehaviors[i].gameObject.transform.position.x,
                _vehicleBehaviors[i].gameObject.transform.position.y,
                _vehicleBehaviors[i].gameObject.transform.position.z);
            _initRotations[i] = Quaternion.Euler(_vehicleBehaviors[i].transform.rotation.eulerAngles);
            _initDriveDirections[i] = _vehicleBehaviors[i].direction;
            _previousPositions[i] = new Vector3(_vehicleBehaviors[i].gameObject.transform.position.x,
                _vehicleBehaviors[i].gameObject.transform.position.y,
                _vehicleBehaviors[i].gameObject.transform.position.z);
        }

	}
	
	// Update is called once per frame
	void Update () {
	    
        for(int i = 0; i < _vehicleBehaviors.Length; i++)
        {
            _respawnCounters[i] += Time.deltaTime;

            float distanceToLastPosition = Vector3.Distance(transform.position, _previousPositions[i]);

            if (_respawnCounters[i] >= _secondsToRespawn && distanceToLastPosition < 0.1f)
            {
                Collider col = _vehicleBehaviors[i].GetComponent<Collider>();
                if(!isInsideCameraFrustrum(col))
                {
                    spawnNewVehicle(i);
                    _respawnCounters[i] -= _secondsToRespawn;
                }
                else
                {
                    _respawnCounters[i] -= _secondsToRespawn;
                }
            }

            _previousPositions[i] = new Vector3(_vehicleBehaviors[i].gameObject.transform.position.x,
                _vehicleBehaviors[i].gameObject.transform.position.y,
                _vehicleBehaviors[i].gameObject.transform.position.z);
        }

	}

    private bool isInsideCameraFrustrum(Collider col)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }

    private void spawnNewVehicle(int index)
    {
        int r = Random.Range(0, _vehicles.Length);
        GameObject newVehicle = Instantiate(_vehicles[r]);
        VehicleBehavior newVehicleBehaviour = newVehicle.GetComponent<VehicleBehavior>();

        newVehicle.gameObject.transform.position = _initPositions[index];
        newVehicle.gameObject.transform.rotation = _initRotations[index];
        newVehicleBehaviour.target = _initTargets[index];
        newVehicleBehaviour.direction = _initDriveDirections[index];

        Destroy(_vehicleBehaviors[index].gameObject);
        _vehicleBehaviors[index] = newVehicleBehaviour;

        Debug.Log("Vehicle at [" + index + "] has been respawned");

    }
}
