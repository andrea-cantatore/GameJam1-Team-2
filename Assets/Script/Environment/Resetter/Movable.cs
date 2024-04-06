using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private Vector3 _originalPos;
    
    private void Start()
    {
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
    
    private void ResetPos()
    {
        transform.position = _originalPos;
    }
}
