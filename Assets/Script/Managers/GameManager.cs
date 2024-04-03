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
            SwitchToUiInput();
        }
        else
        {
            GameIsPaused = !GameIsPaused;
            UIManager.OnResume?.Invoke();
            SwitchToPlayerInput();
        }
        
    }
    private static void SwitchToUiInput()
    {
        InputManager.actionMap.PlayerInput.Disable();
        
    }
    private void SwitchToPlayerInput()
    {
        
        InputManager.actionMap.PlayerInput.Enable();
    }
}
