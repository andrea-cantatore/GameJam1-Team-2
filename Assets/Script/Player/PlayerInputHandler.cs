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
        InputManager.actionMap.PlayerInput.Jump.started += Jump;
        InputManager.actionMap.PlayerInput.Dash.started += Dash;
        InputManager.actionMap.PlayerInput.Interaction.performed += Interact;
        InputManager.actionMap.PlayerInput.GroundPound.performed += GroundPound;
        EventManager.OnDoubleJumpUnlock += DoubleJumpUnlock;
        EventManager.OnGroundPoundUnlock += GroundPoundUnlock;
        EventManager.OnDoubleDashUnlock += DoubleDashUnlock;
    }
    private void OnDisable()
    {
        InputManager.actionMap.PlayerInput.Jump.started -= Jump;
        InputManager.actionMap.PlayerInput.Dash.started -= Dash;
        InputManager.actionMap.PlayerInput.Interaction.performed -= Interact;
        InputManager.actionMap.PlayerInput.GroundPound.performed -= GroundPound;
        EventManager.OnDoubleJumpUnlock -= DoubleJumpUnlock;
        EventManager.OnGroundPoundUnlock -= GroundPoundUnlock;
        EventManager.OnDoubleDashUnlock -= DoubleDashUnlock;
    }
    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _playerController.Jump(_isDoubleJumpUnlocked);
    }
    
    private void Dash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(_isDoubleDashUnlocked)
            _playerController.DoubleDash();
        else
            _playerController.Dash();
    }
    
    private void GroundPound(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(_isGroundPoundUnlocked)
            _playerController.GroundPound();
    }
    
    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    private void GroundPoundUnlock(bool isUnlocked)
    {
        _isGroundPoundUnlocked = isUnlocked;
    }
    private void DoubleJumpUnlock(bool isUnlocked)
    {
        _isDoubleJumpUnlocked = isUnlocked;
    }

    private void DoubleDashUnlock(bool isUnlocked)
    {
        _isDoubleDashUnlocked = isUnlocked; 
    }
}
