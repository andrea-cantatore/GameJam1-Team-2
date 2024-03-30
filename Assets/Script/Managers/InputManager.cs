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
    }
    
}
