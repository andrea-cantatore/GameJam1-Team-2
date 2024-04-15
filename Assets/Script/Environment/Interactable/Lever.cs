using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject[] _interactionObject;
    private bool _isActivated, _isActivable;
    private float _activationTime = 0.5f;
    private float _activationTimer;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private AudioData _audioData;
    AudioClip _leverSFX;
    
    private void Start()
    {
        _leverSFX = _audioData.sfx_leverSound;
    }

    private void Update()
    {
        if(!_isActivable)
        {
            _activationTimer += Time.deltaTime;
            if (_activationTimer >= _activationTime)
            {
                _isActivable = true;
                _activationTimer = 0;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            if(player.IsInteracting() && !_isActivated && _isActivable)
            {
                foreach (var obj in _interactionObject)
                {
                    if(obj.TryGetComponent(out IInteract interactable))
                        interactable.interact(true);
                }
                _animator.SetBool("StartAnimation", true);
                _isActivated = true;
                _isActivable = false;
                _leverSFX = _audioData.sfx_leverSound;
            }
            else if(player.IsInteracting() && _isActivated && _isActivable)
            {
                foreach (var obj in _interactionObject)
                {
                    if(obj.TryGetComponent(out IInteract interactable))
                        interactable.interact(false);
                }
                _animator.SetBool("StartAnimation", false);
                _isActivated = false;
                _isActivable = false;
                _leverSFX = _audioData.sfx_leverSound;
            }
        }
    }
}
