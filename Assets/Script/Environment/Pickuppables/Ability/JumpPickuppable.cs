using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickuppable : MonoBehaviour
{
    [SerializeField] AudioData AudioData;
    AudioClip clip;

    private void Awake()
    {
        clip = AudioData.sfx_pickupSound;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            Debug.Log("Double dash unlocked!");
            EventManager.OnDoubleJumpUnlock?.Invoke(true);
            AudioManager.instance.PlaySFX(clip, transform);
            transform.position = new Vector3(0, -100, 0);
        }
    }
    
}
