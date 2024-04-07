using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private GameObject[] _interactionObject;
    private bool _isActivated, _isActivable;
    private float _activationTime = 1f;
    private float _activationTimer;

    private void Update()
    {
        if(_isActivable)
        {
            _activationTimer -= Time.deltaTime;
        }
        else
        {
            ;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            if(player.IsInteracting() && !_isActivated)
            {
                Debug.Log("Lever activated");
                _isActivated = true;
            }
            else if(player.IsInteracting() && _isActivated)
            {
                Debug.Log("Lever deactivated");
                _isActivated = false;
            }
        }
    }
}
