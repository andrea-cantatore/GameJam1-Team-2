using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpikes : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] AudioData _audioData;
    AudioClip _spikeSFX;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            _animator.SetBool("StartAnimation", true);
            AudioManager.instance.PlaySFX(_audioData.sfx_spikeSound, transform);
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
