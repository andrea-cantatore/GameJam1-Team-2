 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public static ActionMap actionMap;

    
    static InputManager()
    {
        actionMap = new ActionMap();
        actionMap.Enable();
        actionMap.Menu.Disable();
    }

    public static void SwitchToMenuInput()
    {
        
        actionMap.PlayerInput.Disable();
        actionMap.Menu.Enable();
        
    }

    public static void SwitchToPlayerInput()
    { 
        actionMap.PlayerInput.Enable();
        actionMap.Menu.Disable();
    }

   

    
    
}
