using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Player Events
    public static Action OnPlayerDeath;
    public static Action<int> OnPlayerChangeHp;
    public static Action<int> OnPlayerChangeHpNotHiFrame;
    
    
    // Pickuppable Events
    public static Action<bool> OnDoubleJumpUnlock;
    public static Action<bool> OnDoubleDashUnlock;
    public static Action<bool> OnGroundPoundUnlock;
    public static Action OnKeyCollected;

    // Reset event
    public static Action OnResetStarted;
    public static Action OnResetCanceled;
    public static Action OnReset;

    public static Action OnPause;
    public static Action OnResume;
    public static Action UnPauseToggle;

    // Dialog Events
    public static Action OnInteracting;
    public static Action<string> OnEnterDialogue;
    public static Action OnExitDialogue;

    // Timer Events
    public static Action<int> OnTimerStarted; // chiama questo per far partire il timer(l'ho pensato in modo che gli venga passato un tempo diverso a seconda del bottone)
    public static Action OnTimerEnded;
    public static Action OnTimerCanceled; // chiama questo se avete deciso che ripremendo il bottone il timer si fermi ( torner� a zero)

}
