using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickuppable : MonoBehaviour
{
    [SerializeField] AudioData AudioData;
    AudioClip clip;
    private Transform _originalPos;
    [SerializeField] private GameObject _door, _lock;

    private void Awake()
    {
        clip = AudioData.sfx_pickupSound;
    }

    private void Start()
    {
        _originalPos = transform;
        Debug.Log("boh");
    }

    private void OnEnable()
    {
        EventManager.OnReset += ResetPos;
    }
    private void OnDisable()
    {
        EventManager.OnReset -= ResetPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            Debug.Log("Key Obtained");
            EventManager.OnKeyCollected?.Invoke();
            transform.position = new Vector3(0, -100, 0);
            
            if(_door.TryGetComponent(out IInteract door))
                door.interact(true);
            if(_lock.TryGetComponent(out IInteract lockk))
                lockk.interact(true);
            
            AudioManager.instance.PlaySFX(clip, transform);
        }
    }
    
    private void ResetPos()
    {
        transform.position = _originalPos.position;
    }
}
