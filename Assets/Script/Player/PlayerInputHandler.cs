using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, IPlayer
{
    private PlayerController _playerController;
    
    private bool _isGroundPoundUnlocked;
    private bool _isDoubleJumpUnlocked;
    private bool _isDoubleDashUnlocked;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        InputManager.actionMap.PlayerInput.Jump.performed += Jump;
        InputManager.actionMap.PlayerInput.Dash.performed += Dash;
        InputManager.actionMap.PlayerInput.Interaction.performed += Interact;
        InputManager.actionMap.PlayerInput.GroundPound.performed += GroundPound;
    }
    private void OnDisable()
    {
        InputManager.actionMap.PlayerInput.Jump.performed -= Jump;
        InputManager.actionMap.PlayerInput.Dash.performed -= Dash;
        InputManager.actionMap.PlayerInput.Interaction.performed -= Interact;
        InputManager.actionMap.PlayerInput.GroundPound.performed -= GroundPound;
    }
    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerController.Jump();
        }
    }
    
    private void Dash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(_isDoubleDashUnlocked)
                _playerController.DoubleDash();
            else
                _playerController.Dash();
        }
    }
    
    private void GroundPound(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(_isGroundPoundUnlocked)
            _playerController.GroundPound();
    }
    
    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    public void GroundPoundUnlock()
    {
        _isGroundPoundUnlocked = true;
    }
    public void DoubleJumpUnlock()
    {
        _isDoubleJumpUnlocked = true;
    }
    public void DoubleDashUnlock()
    {
        //_isDoubleDashUnlocked = true; DON'T UNCOMMENT THIS LINE YET I NEED TO FIX THE DOUBLE DASH FIRST
    }
}
