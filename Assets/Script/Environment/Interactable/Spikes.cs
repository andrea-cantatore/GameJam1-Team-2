using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out IPlayer player))
        {
            EventManager.OnPlayerChangeHp(-1);
        }
    }
}
