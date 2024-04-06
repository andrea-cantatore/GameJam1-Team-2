using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    
    private bool _isGameResetting;
    private float _resetTimer;
    [SerializeField] private float _resetDuration = 3f;
    
    bool GameIsPaused;
    private void OnEnable()
    {
        EventManager.UnPauseToggle += () => GameIsPaused = false;
        InputManager.actionMap.UI_Toggle.Toggle.performed += Pause;
        InputManager.actionMap.Menu.Disable();
        EventManager.OnResetStarted += ResetStarted;
        EventManager.OnResetCanceled += ResetCanceled;
    }
    
    private void OnDisable()
    {
        
        InputManager.actionMap.UI_Toggle.Toggle.performed -= Pause;
        EventManager.OnResetStarted -= ResetStarted;
        EventManager.OnResetCanceled -= ResetCanceled;
    }

    private void Update()
    {
        if(_isGameResetting && !GameIsPaused)
        {
            _resetTimer += Time.deltaTime;
            if(_resetTimer >= _resetDuration)
            {
                Debug.Log("Resetting");
                EventManager.OnReset?.Invoke();
                _isGameResetting = false;
                _resetTimer = 0;
            }
        }
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
    
    private void ResetStarted()
    {
        Debug.Log("Reset Started");
        _isGameResetting = true;
    }
    
private void ResetCanceled()
    {
        Debug.Log("Reset Canceled");
        _isGameResetting = false;
        _resetTimer = 0;
    }
    
}
