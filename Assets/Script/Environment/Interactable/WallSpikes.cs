using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpikes : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            _animator.SetBool("StartAnimation", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            _animator.SetBool("StartAnimation", false);
        }
    }
}
