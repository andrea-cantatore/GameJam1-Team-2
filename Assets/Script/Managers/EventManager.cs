using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    
    
    
    // Pickuppable Events
    public static Action<bool> OnDoubleJumpUnlock;
    public static Action<bool> OnDoubleDashUnlock;
    public static Action<bool> OnGroundPoundUnlock;

    // Reset event
    public static Action OnReset;
    public static Action OnResetCanceled;
    
}
