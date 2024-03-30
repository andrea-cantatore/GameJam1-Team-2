using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.actionMap.PlayerInput.Pause.performed += Pause;
        InputManager.actionMap.UI.Resume.performed += Pause;
    }
    
    private void OnDisable()
    {
        InputManager.actionMap.PlayerInput.Pause.performed -= Pause;
        InputManager.actionMap.UI.Resume.performed -= Pause;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if(InputManager.actionMap.PlayerInput.enabled)
        {
            SwitchToUiInput();
        }
        else
        {
            SwitchToPlayerInput();
        }
        
    }
    private static void SwitchToUiInput()
    {
        InputManager.actionMap.PlayerInput.Disable();
        InputManager.actionMap.UI.Enable();
    }
    private void SwitchToPlayerInput()
    {
        InputManager.actionMap.UI.Disable();
        InputManager.actionMap.PlayerInput.Enable();
    }
}
