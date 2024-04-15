using System;
using UnityEngine;

public class WallTrapSound : MonoBehaviour
{
    [SerializeField] private AudioData _audioData;
    AudioClip _spikeSFX;
    float _clipLength;
    float _timer;

    private void Start()
    {
        _spikeSFX = _audioData.sfx_spikeSound;
        _clipLength = _spikeSFX.length;
        AudioManager.instance.PlaySFX(_spikeSFX, transform);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _clipLength + 1.8f)
        {
            AudioManager.instance.PlaySFX(_spikeSFX, transform);
            _timer = 0;
        }
    }
}
