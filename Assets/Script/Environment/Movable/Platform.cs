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
    private Vector3 _originalPos;
    
    private void Start()
    {
        _currentWaypointDestination = 1;
        _originalPos = transform.position;
    }
    
    private void OnEnable()
    {
        EventManager.OnReset += ResetPos;
    }
    private void OnDisable()
    {
        EventManager.OnReset -= ResetPos;
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
    
    private void ResetPos()
    {
        transform.position = _originalPos;
    }
    
    public void interact(bool isActivated)
    {
        _isActivated = isActivated;
    }
    
}
