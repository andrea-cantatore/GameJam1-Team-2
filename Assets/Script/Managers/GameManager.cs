using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    

    bool GameIsPaused;
    private void OnEnable()
    {
        UIManager.UnPauseToggle += () => GameIsPaused = false;
        InputManager.actionMap.UI_Toggle.Toggle.performed += Pause;
        InputManager.actionMap.Menu.Disable();
    }
    
    private void OnDisable()
    {
        
        InputManager.actionMap.UI_Toggle.Toggle.performed -= Pause;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        
        if(GameIsPaused == false)
        {
            GameIsPaused = true;
            UIManager.OnPause?.Invoke();
            InputManager.SwitchToMenuInput();
        }
            
        else
        {
            GameIsPaused = !GameIsPaused;
            UIManager.OnResume?.Invoke();
            InputManager.SwitchToPlayerInput();
        }
            
        
    }
    
}
