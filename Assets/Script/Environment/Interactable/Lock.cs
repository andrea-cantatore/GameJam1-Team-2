using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IInteract
{
    [SerializeField] private Animator _animator;


    public void interact(bool isActivated)
    {
        if(isActivated)
        {
            _animator.SetBool("StartAnimation", true);
        }
        else
        {
            _animator.SetBool("StartAnimation", false);
        }
    }
}
