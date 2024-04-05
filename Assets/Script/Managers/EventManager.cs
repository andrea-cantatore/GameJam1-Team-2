using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    
    
    
    // Pickuppable Events
    public static Action<bool> OnDoubleJumpUnlock;
    public static Action<bool> OnDoubleDashUnlock;
    public static Action<bool> OnGroundPoundUnlock;

    // Reset event
    public static Action OnResetStarted;
    public static Action OnResetCanceled;
    public static Action OnResetCompleted;

    public static Action OnPause;
    public static Action OnResume;
    public static Action UnPauseToggle;

}
