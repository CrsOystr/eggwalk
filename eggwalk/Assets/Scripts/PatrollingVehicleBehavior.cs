using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrollingVehicleBehavior : MonoBehaviour {
    
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceStartTurning;
    [SerializeField] private List<Transform> _pathTransforms;

    private List<Vector3> _pathPoints; // _pathTransforms will be converted into a list of Vector3s, because:
    private float _startY; // the y of the vehicle will be different than the y value of the target, we want to keep the y value the same
    private int _pathPointIndex;

    private Vector3 NextPathPoint
    {
        get
        {
            if(_pathPointIndex < _pathPoints.Count - 1)
            {
                return _pathPoints[_pathPointIndex + 1];
            }
            else
            {
                return _pathPoints[0];
            }
        }
    }

    void Start()
    {
        _startY = transform.position.y;
        _pathPointIndex = 0;

        _pathPoints = new List<Vector3>();
        for (int i = 0; i < _pathTransforms.Count; i++)
        {
            Vector3 toAdd = new Vector3(_pathTransforms[i].position.x, _startY, _pathTransforms[i].position.z);
            _pathPoints.Add(toAdd);
        }

        transform.rotation = Quaternion.LookRotation((_pathPoints[_pathPointIndex] - transform.position), Vector3.up); // start out looking at the first target
    }

    void Update()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, _pathPoints[_pathPointIndex], _speed * Time.deltaTime);
        transform.position = newPos;

        
        float distanceToTarget = Vector3.Distance(transform.position, _pathPoints[_pathPointIndex]);
        if(distanceToTarget <= _distanceStartTurning) // smoothly rotate the vehicle to look at the next target
        {
            RotateTowardTarget(NextPathPoint);
        }
        else // look at the current target
        {
            RotateTowardTarget(_pathPoints[_pathPointIndex]);
        }

        // if we've reached our target, go to the next one
        if(transform.position == _pathPoints[_pathPointIndex])
        {
            _pathPointIndex++;
            if(_pathPointIndex >= _pathPoints.Count)
            {
                _pathPointIndex = 0;
            }
        }
    }

    private void RotateTowardTarget(Vector3 target)
    {
        Vector3 toTarget = target - transform.position; // vector from the vehicle to the target point
        Quaternion lookAtNextPoint = Quaternion.LookRotation(toTarget, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, lookAtNextPoint, 0.05f);
        transform.rotation = newRotation;
    }

}
