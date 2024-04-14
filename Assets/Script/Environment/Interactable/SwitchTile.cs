using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTile : MonoBehaviour
{
    [SerializeField] private GameObject[] _interactionObject;
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            foreach (var obj in _interactionObject)
            {
                if(obj.TryGetComponent(out IInteract interactable))
                    interactable.interact(true);
            }
            _animator.SetBool("StartAnimation", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            foreach (var obj in _interactionObject)
            {
                if(obj.TryGetComponent(out IInteract interactable))
                    interactable.interact(false);
            }
            _animator.SetBool("StartAnimation", false);
        }
    }
}
