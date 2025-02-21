using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTile : MonoBehaviour
{
    [SerializeField] private GameObject[] _interactionObject;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private AudioData _audioData;
    AudioClip _switchSFX;

    private void Start()
    {
        _switchSFX = _audioData.sfx_floorSwitchSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            foreach (var obj in _interactionObject)
            {
                if(obj.TryGetComponent(out IInteract interactable))
                    interactable.interact(true);
            }
            _animator.SetBool("StartAnimation", true);
            AudioManager.instance.PlaySFX(_switchSFX, transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            foreach (var obj in _interactionObject)
            {
                if(obj.TryGetComponent(out IInteract interactable))
                    interactable.interact(false);
            }
            _animator.SetBool("StartAnimation", false);
            AudioManager.instance.PlaySFX(_switchSFX, transform);
        }
    }
}
