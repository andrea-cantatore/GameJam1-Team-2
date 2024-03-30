using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody _rb;

    private bool _isGrounded = true, _isDashing, _isInteracting;

    [Header("Movement Forces")] 
    [SerializeField] private float _jumpForce = 300f;

    [SerializeField] private float _moveVelocity = 10f;
    
    [Header("Dash values")] 
    [SerializeField] private float _dashForce = 300f;
    [SerializeField] private float _dashCooldown = 1f;
    private float _dashTimer;
    
    
    
    private void OnEnable()
    {
        InputManager.actionMap.PlayerInput.Jump.performed += Jump;
        InputManager.actionMap.PlayerInput.Dash.performed += Dash;
        InputManager.actionMap.PlayerInput.Interaction.performed += Interact;
    }
    private void OnDisable()
    {
        InputManager.actionMap.PlayerInput.Jump.performed -= Jump;
        InputManager.actionMap.PlayerInput.Dash.performed -= Dash;
        InputManager.actionMap.PlayerInput.Interaction.performed -= Interact;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
        Move();
        if(_isDashing)
            DashCooldown();
    }

    private void Move()
    {
        if(_isDashing) return;
        Vector2 movement = InputManager.actionMap.PlayerInput.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = transform.forward * movement.y + transform.right * movement.x;
        moveDirection = Vector3.Normalize(moveDirection);
        Vector3 velocity = moveDirection * _moveVelocity * Time.deltaTime;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if(_isDashing) return;
        _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
        _isDashing = true;
    }
    private void DashCooldown()
    {
        _dashTimer += Time.deltaTime;
        if(_dashTimer < _dashCooldown) return;
        _isDashing = false;
        _dashTimer = 0;
    }

    private void Interact(InputAction.CallbackContext context)
    {
    }
}
