using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    
    private bool _isGameResetting;
    private float _resetTimer;
    public bool _PlayerHasKey; // da usare nel momento in cui serve il check della chiave
    
    bool GameIsPaused;
    private void OnEnable()
    {
        Time.timeScale = 1;
        InputManager.SwitchToPlayerInput();
        EventManager.UnPauseToggle += () => GameIsPaused = false;
        InputManager.actionMap.UI_Toggle.Toggle.performed += Pause;
        InputManager.actionMap.Menu.Disable();
        EventManager.OnKeyCollected += () => _PlayerHasKey = false;
        InputManager.actionMap.UI_Toggle.Enable();
    }
    
    private void OnDisable()
    {
        InputManager.actionMap.UI_Toggle.Toggle.performed -= Pause;
    }

    private void Start()
    {
        _PlayerHasKey = false;
        Application.targetFrameRate = 60;
        
    }


    private void Pause(InputAction.CallbackContext context)
    {
        
        if(GameIsPaused == false)
        {
            GameIsPaused = true;
            EventManager.OnPause?.Invoke();
            InputManager.SwitchToMenuInput();
        }
            
        else
        {
            GameIsPaused = !GameIsPaused;
            EventManager.OnResume?.Invoke();
            InputManager.SwitchToPlayerInput();
        }
    }
    
    
}
