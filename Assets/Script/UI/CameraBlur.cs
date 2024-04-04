using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
[RequireComponent(typeof(PostProcessLayer))]
public class CameraBlur : MonoBehaviour
{
    // events for blur trigger
    public static Action OnBlurBg;
    public static Action OnBlurOff;

    PostProcessVolume pp_volume;
 

    private void Awake()
    {
        pp_volume = GetComponent<PostProcessVolume>();
    }

    private void OnEnable()
    {
        OnBlurBg += TurnBlurOn;
        OnBlurOff += TurnBlurOff;
    }

    private void OnDisable()
    {
        OnBlurBg -= TurnBlurOn;
        OnBlurOff -= TurnBlurOff;
    }


    private void Start()
    {
        pp_volume.enabled = false;
    }

    void TurnBlurOn()
    {
        pp_volume.enabled = true;
    }

    void TurnBlurOff()
    {
        pp_volume.enabled = false;
    }
    
}
