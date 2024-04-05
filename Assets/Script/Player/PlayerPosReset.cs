using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosReset : MonoBehaviour
{
    private Vector3 _resetPos;
    
    private void Awake()
    {
        _resetPos = transform.position;
    }
    
    public void ResetPosSetter(Transform resetPos)
    {
        _resetPos = resetPos.position;
    }
    
    public void ResetPos()
    {
        transform.position = _resetPos;
    }
}
