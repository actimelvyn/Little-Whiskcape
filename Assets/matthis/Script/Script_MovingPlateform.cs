using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MovingPlateform : MonoBehaviour
{
    [SerializeField]
    private Script_WaypointPath _WaypointPath;

    [SerializeField]
    private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private void Start()
    {
        TargetNextWaypoint();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsePercentage = _elapsedTime / _timeToWaypoint;
        elapsePercentage = Mathf.SmoothStep(0, 1, elapsePercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsePercentage);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsePercentage);

        if (elapsePercentage >= 1) 
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _WaypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _WaypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _WaypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed; 
    }
        private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}
