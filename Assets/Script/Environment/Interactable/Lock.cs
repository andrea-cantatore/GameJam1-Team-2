using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IInteract
{
    [SerializeField] private Animator _animator;
    [SerializeField] AudioData _audioData;
    AudioClip _lockSFX;
    
    private void Start()
    {
        _lockSFX = _audioData.sfx_openingLockSound;
    }


    public void interact(bool isActivated)
    {
        if(isActivated)
        {
            _animator.SetBool("StartAnimation", true);
            AudioManager.instance.PlaySFX(_lockSFX, transform);
        }
        else
        {
            _animator.SetBool("StartAnimation", false);
        }
    }
}
