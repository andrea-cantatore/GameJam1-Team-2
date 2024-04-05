using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    
    
    
    // Pickuppable Events
    public static Action<bool> OnDoubleJumpUnlock;
    public static Action<bool> OnDoubleDashUnlock;
    public static Action<bool> OnGroundPoundUnlock;
    
}
