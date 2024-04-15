using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour, IInteract
{
    private bool _isOpen = false;
    
    

    public void interact(bool isActivated)
    {
        _isOpen = isActivated;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player) && _isOpen)
            EventManager.OnPlayerWin?.Invoke();
    }
}
