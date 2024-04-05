using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "ScriptableObj/Audio")]
public class AudioData : ScriptableObject
{
    [Header("Player Sounds")]
    [SerializeField] public AudioClip walkingSound;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip landingSound;
    [SerializeField] public AudioClip dashSound;
    [SerializeField] public AudioClip groundPoundSound;
    [Header("Environment Sounds")]
    [SerializeField] public AudioClip lavaSound;
    [SerializeField] public AudioClip voidSound;
    [SerializeField] public AudioClip spikeSound;
    [SerializeField] public AudioClip dartsSound;
    [SerializeField] public AudioClip fireBreathSound;
    [SerializeField] public AudioClip openingDoorSound;
    [SerializeField] public AudioClip openingLockSound;
    [SerializeField] public AudioClip pushingCrateSound;
    [SerializeField] public AudioClip leverSound;
    [SerializeField] public AudioClip buttonSound;
    [SerializeField] public AudioClip floorSwitchSound;
    [SerializeField] public AudioClip timerSound;
    [SerializeField] public AudioClip pickupSound;
    [Header("Event Sounds")]
    [SerializeField] public AudioClip deathSound;
    [SerializeField] public AudioClip victorySound;
    [SerializeField] public AudioClip looseSound;
    public int prova;


}
