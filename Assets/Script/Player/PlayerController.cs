using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("General")] private Transform _cameraTransform;
    private Rigidbody _rb;
    private bool _isInteracting;
    [SerializeField] private LayerMask _groundLayer;
    

    [Header("Movement")] [SerializeField] private float _jumpForce = 300f;
    [SerializeField] private float _groundPoundForce = 300f;
    [SerializeField] private float _moveVelocity = 10f;
    private bool _isGroundPounding;
    private bool _isDoubleJumping;
    [Header("Dash values")] [SerializeField]
    private float _dashForce = 300f;

    [SerializeField] private float _dashDuration = 1f, _dashCooldown = 2f;
    private float _dashTimer, _dash2Timer, _dashCooldownTimer;
    private bool _isDashing, _isDashingAirborne, _isDashUsable = true, _isDoubleDashing;

    private bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, 1.1f, _groundLayer);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        Move();
        
        if (!_isDashUsable || _isDoubleDashing)
            DashCooldown();
        if (_isGroundPounding)
            _isGroundPounding = !IsGrounded;
    }

    private void Move()
    {
        if (_isDashing || _isDoubleDashing) return;

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

    public void Jump(bool UnlockedDoubleJump)
    {
        if (IsGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isDoubleJumping = false;
        }
        else if(!_isDoubleJumping && UnlockedDoubleJump)
        {
            if(_rb.velocity.y < 0)
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isDoubleJumping = true;
        }
    }
    
    public void Dash()
    {
        if (!_isDashUsable) return;
        _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
        _isDashing = true;
        _isDashUsable = false;
        _isDashingAirborne = !IsGrounded;
    }
    public void DoubleDash()
    {
        if (!_isDashing && _isDashUsable)
        {
            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
            _isDashing = true;
            _isDashUsable = false;
        }
        else if (!_isDoubleDashing)
        {
            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
            _isDoubleDashing = true;
        }
        
        _isDashingAirborne = !IsGrounded;
    }
    private void DashCooldown()
    {
        if (_isDashing)
        {
            _dashTimer += Time.deltaTime;
            if (_dashTimer > _dashDuration)
            {
                _isDashing = false;
                _dashTimer = 0;
            }
        }
        else if (_isDoubleDashing)
        {
            _dash2Timer += Time.deltaTime;
            if (_dash2Timer > _dashDuration)
            {
                _isDoubleDashing = false;
                _dash2Timer = 0;
            }
        }
        if (_isDashingAirborne)
        {
            if (IsGrounded)
            {
                _isDashUsable = true;
            }

            return;
        }

        _dashCooldownTimer += Time.deltaTime;
        if (_dashCooldownTimer > _dashCooldown)
        {
            _isDashUsable = true;
            _dashCooldownTimer = 0;
        }
    }

    public void GroundPound()
    {
        if (_isGroundPounding || IsGrounded) return;

        _isGroundPounding = true;
        _rb.AddForce(Vector3.down * _groundPoundForce, ForceMode.Impulse);
        if (_isDashing)
            _isDashing = false;
    }
    
    
}
