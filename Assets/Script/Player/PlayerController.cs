using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IPlayer
{
    [Header("General")]
    
    private Transform _cameraTransform;
    private Rigidbody _rb;
    private bool _isInteracting;
    [SerializeField] private LayerMask _groundLayer;
    
    
    [Header("Movement")] 
    
    [SerializeField] private float _jumpForce = 300f;
    [SerializeField] private float _moveVelocity = 10f;
    private bool _isGroundPounding;
    [HideInInspector] public bool _isGroundPoundUnlocked;
    
    [Header("Dash values")] 
    
    [SerializeField] private float _dashForce = 300f;
    [SerializeField] private float _dashCooldown = 1f;
    private float _dashTimer;
    private bool _isDashing;
    
    private bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, 1.1f, _groundLayer);
    
    
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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        Move();
        if(_isDashing)
            DashCooldown();
        if (_isGroundPounding)
            _isGroundPounding = !IsGrounded;
        
        Debug.Log(_isGroundPoundUnlocked);

    }

    private void Move()
    {
        if(_isDashing) return;
        Vector2 movement = InputManager.actionMap.PlayerInput.Movement.ReadValue<Vector2>();
        Vector3 cameraForwardNoY = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z);
        Vector3 moveDirection = cameraForwardNoY * movement.y + _cameraTransform.right * movement.x;
        moveDirection = Vector3.Normalize(moveDirection);
        Vector3 velocity = moveDirection * _moveVelocity * Time.deltaTime;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        if (movement != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * _moveVelocity);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
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
    
    private void GroundPound(InputAction.CallbackContext context)
    {
        if(!_isGroundPoundUnlocked || 
           _isGroundPounding || 
           IsGrounded) return;
        
        _isGroundPounding = true;
        _rb.AddForce(Vector3.down * _jumpForce, ForceMode.Impulse);
        if(_isDashing)
            _isDashing = false;
    }

    private void Interact(InputAction.CallbackContext context)
    {
    }

    public void GroundPoundUnlock()
    {
        _isGroundPoundUnlocked = true;
    }
}


