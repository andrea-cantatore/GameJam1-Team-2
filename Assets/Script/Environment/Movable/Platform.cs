using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour, IInteract
{
    private bool _isActivated;
    [SerializeField] private Transform[] _waypoints;
    private int _currentWaypointDestination;
    
    private void Start()
    {
        _currentWaypointDestination = 1;
    }
    private void Update()
    {
        if (_isActivated)
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointDestination].position, 5f * Time.deltaTime);
            if (Vector3.Distance(transform.position, _waypoints[_currentWaypointDestination].position) < 1f)
            {
                _currentWaypointDestination++;
                if (_currentWaypointDestination >= _waypoints.Length)
                {
                    _currentWaypointDestination = 0;
                }
            }
        }
    }
    

    public void interact(bool isActivated)
    {
        _isActivated = isActivated;
    }
    
}
