using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Player Events
    public static Action OnPlayerDeath;
    public static Action<int> OnPlayerChangeHp;
    
    
    // Pickuppable Events
    public static Action<bool> OnDoubleJumpUnlock;
    public static Action<bool> OnDoubleDashUnlock;
    public static Action<bool> OnGroundPoundUnlock;

    // Reset event
    public static Action OnResetStarted;
    public static Action OnResetCanceled;
    public static Action OnReset;
    
}
