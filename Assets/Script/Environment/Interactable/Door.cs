using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] Animator animator1;
    [SerializeField] Animator animator2;
    [SerializeField] private bool _isDoubleInteraction;
    private int _interactionCount = 0;
    [SerializeField] private AudioData _audioData;
    AudioClip _clip;

    private void Start()
    {
        _clip = _audioData.sfx_openingDoorSound;
    }

    public void interact(bool isActivated)
    {
        if(isActivated)
        {
            if (_isDoubleInteraction)
            {
                if (_interactionCount < 2)
                {
                    _interactionCount++;
                }
                else
                {
                    animator1.SetBool("Open", true);
                    animator2.SetBool("Open", true);
                    AudioManager.instance.PlaySFX(_clip, transform);
                }
            }
            else
            {
                animator1.SetBool("Open", true);
                animator2.SetBool("Open", true);
                AudioManager.instance.PlaySFX(_clip, transform);
            }
        }
        else
        {
            if (_isDoubleInteraction)
            {
                _interactionCount--;
                animator1.SetBool("Open", false);
                animator2.SetBool("Open", false);
            }
            else
            {
                animator1.SetBool("Open", false);
                animator2.SetBool("Open", false);
            }
        }
    }
}
