using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleBehavior : MonoBehaviour {

	public enum DriveDirection {
		NORTH, SOUTH, EAST, WEST
	}

    public bool debug;

    [SerializeField] private Transform _target;
    [SerializeField] private float _minSpeed, _maxSpeed, _force;
    [SerializeField] private DriveDirection _driveDirection;
    [SerializeField] private float _distanceThreshold;

    private Rigidbody _rigidBody;
    private Transform _prevTarget;
    private VehicleManager _vehicleManager;

    public VehicleManager VehicleManager
    {
        get { return _vehicleManager; }
        set { _vehicleManager = value; }
    }

    public DriveDirection Direction
    {
        get { return _driveDirection; }
        set { _driveDirection = value; }
    }

    public Transform Target {
        get { return _target; }
        set { _target = value; }
    }

    void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_target == null) return;

        Vector3 toTarget = _target.position - transform.position;
        //_rigidBody.velocity = toTarget.normalized * _speed * Time.deltaTime;

        Vector3 force = toTarget.normalized * _force;
        _rigidBody.AddForce(force);

        if (_rigidBody.velocity.magnitude < _minSpeed)
        {
            Vector3 adjVelocity = _rigidBody.velocity.normalized * _minSpeed;
            _rigidBody.velocity = adjVelocity;
        }
        else if (_rigidBody.velocity.magnitude > _maxSpeed)
        {
            Vector3 adjVelocity = _rigidBody.velocity.normalized * _maxSpeed;
            _rigidBody.velocity = adjVelocity;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(toTarget), 0.1f);

        float distanceToTarget = Vector3.Distance(transform.position, _target.position);
        if (distanceToTarget <= _distanceThreshold)
        {
            _prevTarget = _target;
            ChooseNextTarget();
        }

        /*
        Vector3 newPos = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        transform.position = newPos;

        if(transform.position == _target.position)
        {
            ChooseNextTarget();
        }
        */
    }

    public void ChooseNextTarget()
    {
        // we're going to add our choices to a list and pick one at random
        List<Transform> choices = new List<Transform>();
        List<DriveDirection> potentialDir = new List<DriveDirection>();
        IntersectionBehavior ib = _target.GetComponentInParent<IntersectionBehavior>();

        switch (_driveDirection)
        {
            case VehicleBehavior.DriveDirection.NORTH: // can't choose South
                if (ib.toNorth != null)
                {
                    choices.Add(ib.toNorth.SouthEast);
                    potentialDir.Add(DriveDirection.NORTH);
                }
                if (ib.toEast != null)
                {
                    choices.Add(ib.toEast.SouthWest);
                    potentialDir.Add(DriveDirection.EAST);
                }
                if (ib.toWest != null)
                {
                    choices.Add(ib.toWest.NorthEast);
                    potentialDir.Add(DriveDirection.WEST);
                }
                break;
            case VehicleBehavior.DriveDirection.SOUTH: // can't choose North
                if (ib.toSouth != null)
                {
                    choices.Add(ib.toSouth.NorthWest);
                    potentialDir.Add(DriveDirection.SOUTH);
                }
                if (ib.toEast != null)
                {
                    choices.Add(ib.toEast.SouthWest);
                    potentialDir.Add(DriveDirection.EAST);
                }
                if (ib.toWest != null)
                {
                    choices.Add(ib.toWest.NorthEast);
                    potentialDir.Add(DriveDirection.WEST);
                }
                break;
            case VehicleBehavior.DriveDirection.EAST: // can't choose West
                if (ib.toNorth != null)
                {
                    choices.Add(ib.toNorth.SouthEast);
                    potentialDir.Add(DriveDirection.NORTH);
                }
                if (ib.toSouth != null)
                {
                    choices.Add(ib.toSouth.NorthWest);
                    potentialDir.Add(DriveDirection.SOUTH);
                }

                if (ib.toEast != null)
                {
                    choices.Add(ib.toEast.SouthWest);
                    potentialDir.Add(DriveDirection.EAST);
                }
                break;
            case VehicleBehavior.DriveDirection.WEST: // can't choose East
                if (ib.toNorth != null)
                {
                    choices.Add(ib.toNorth.SouthEast);
                    potentialDir.Add(DriveDirection.NORTH);
                }
                if (ib.toSouth != null)
                {
                    choices.Add(ib.toSouth.NorthWest);
                    potentialDir.Add(DriveDirection.SOUTH);
                }
                if (ib.toWest != null)
                {
                    choices.Add(ib.toWest.NorthEast);
                    potentialDir.Add(DriveDirection.WEST);
                }
                break;
        }

        // pick a random choice if possible
        if (choices.Count != 0)
        {
            int r = Random.Range(0, choices.Count);
            // both choices and potential dir will have the same length
            _driveDirection = potentialDir[r];
            _target = choices[r];
            _target.position = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        }
    }

}
