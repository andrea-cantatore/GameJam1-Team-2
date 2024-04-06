using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosReset : MonoBehaviour, IPlayerReset
{
    private Vector3 _resetPos;
    private bool _isJumpUnlocked, _isDashUnlocked, _isGroundPoundUnlocked;
    private bool[] _abilityUnlocks = new bool[3];
    
    private void Awake()
    {
        _resetPos = transform.position;
    }

    private void OnEnable()
    {
        EventManager.OnReset += ResetPos;
        EventManager.OnGroundPoundUnlock += GroundPoundUnlock;
        EventManager.OnDoubleDashUnlock += DoubleDashUnlock;
        EventManager.OnDoubleJumpUnlock += DoubleJumpUnlock;
    }
    private void OnDisable()
    {
        EventManager.OnReset -= ResetPos;
        EventManager.OnGroundPoundUnlock -= GroundPoundUnlock;
        EventManager.OnDoubleDashUnlock -= DoubleDashUnlock;
        EventManager.OnDoubleJumpUnlock -= DoubleJumpUnlock;
    }

    public void ResetPosSetter(Transform resetPos)
    {
        _resetPos = resetPos.position;
        
        if(_isJumpUnlocked)
            _abilityUnlocks[0] = true;
        if(_isDashUnlocked)
            _abilityUnlocks[1] = true;
        if(_isGroundPoundUnlocked)
            _abilityUnlocks[2] = true;
        
    }
    
    public void ResetPos()
    {
        transform.position = _resetPos;
        if(!_abilityUnlocks[0])
            EventManager.OnDoubleJumpUnlock?.Invoke(false);
        if(!_abilityUnlocks[1])
            EventManager.OnDoubleDashUnlock?.Invoke(false);
        if(!_abilityUnlocks[2])
            EventManager.OnGroundPoundUnlock?.Invoke(false);
    }
    
    private void GroundPoundUnlock(bool isUnlocked)
    {
        _isGroundPoundUnlocked = isUnlocked;
    }
    
    private void DoubleDashUnlock(bool isUnlocked)
    {
        _isDashUnlocked = isUnlocked;
    }
    
    private void DoubleJumpUnlock(bool isUnlocked)
    {
        _isJumpUnlocked = isUnlocked;
    }
}
public interface IPlayerReset
{
    void ResetPosSetter(Transform resetPos);
}
