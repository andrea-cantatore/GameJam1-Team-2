using System;

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
    [SerializeField] private LayerMask _pushableLayer;
    [SerializeField] private int _hp = 3;

    [Header("Movement")] [SerializeField] private float _jumpForce = 300f;
    [SerializeField] private float _groundPoundForce = 300f;
    [SerializeField] private float _moveVelocity = 10f;
    public bool IsGroundPounding, CanGroundPound;
    private bool _isDoubleJumping;
    
    [Header("Audio Values")]
    [SerializeField] private AudioData _audioData;

    private AudioClip _walkingSFX, _jumpingSFX, _dashingSFX, _groundPoundSFX;
    private bool _canPlayWalkingSFX = true;
    private float walkingSFXTimer;

    [Header("Dash values")] [SerializeField]
    private float _dashForce = 300f;

    [SerializeField] private float _dashDuration = 1f, _dashCooldown = 2f, _hiFrameDuration = 2f, _hiFrameCooldown = 5f;
    private float _dashTimer, _dash2Timer, _dashCooldownTimer, _hiFrameTimer;
    private bool _isDashing, _isDashingAirborne, _isDashUsable = true, _isDoubleDashing, _isHiFrame, _isHiFrameUsable = true;

    public bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, 1.1f, _groundLayer);

    float dot;
    
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        _walkingSFX = _audioData.sfx_walkingSound;
        _jumpingSFX = _audioData.sfx_jumpSound;
        _dashingSFX = _audioData.sfx_dashSound;
        _groundPoundSFX = _audioData.sfx_groundPoundSound;
    }
    private void OnEnable()
    {
        EventManager.OnPlayerChangeHp += ChangeHp;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerChangeHp -= ChangeHp;
    }
    private void Update()
    {
        Move();

        if (!_isDashUsable || _isDoubleDashing)
            DashCooldown();
        if (IsGroundPounding)
            IsGroundPounding = !IsGrounded;
        if (!_isHiFrameUsable)
             HiFrameCooldown();
        if (!_canPlayWalkingSFX)
        {
            walkingSFXTimer += Time.deltaTime;
            if(walkingSFXTimer >= _walkingSFX.length)
            {
                _canPlayWalkingSFX = true;
                walkingSFXTimer = 0;
            }
        }
        
    }
    private void Move()
    {
        if (_isDashing || _isDoubleDashing) return;

        Vector2 movement = InputManager.actionMap.PlayerInput.Movement.ReadValue<Vector2>();
        Vector3 cameraForwardNoY = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z);
        Vector3 moveDirection = cameraForwardNoY * movement.y + _cameraTransform.right * movement.x;
        moveDirection = Vector3.Normalize(moveDirection);

        PushableChecker(moveDirection);

        Vector3 velocity = moveDirection * _moveVelocity * Time.deltaTime;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        if (movement != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * _moveVelocity);
            if (_canPlayWalkingSFX)
            {
                AudioManager.instance.PlaySFX(_walkingSFX, transform);
                _canPlayWalkingSFX = false;
            }
        }
    }

    public void Jump(bool UnlockedDoubleJump)
    {
        if (IsGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isDoubleJumping = false;
            AudioManager.instance.PlaySFX(_jumpingSFX, transform);
        }
        else if (!_isDoubleJumping && UnlockedDoubleJump)
        {
            if (_rb.velocity.y < 0)
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isDoubleJumping = true;
            AudioManager.instance.PlaySFX(_jumpingSFX, transform);
        }
    }

    public void Dash()
    {
        if (!_isDashUsable) return;
        _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
        _isDashing = true;
        _isDashUsable = false;
        _isDashingAirborne = !IsGrounded;
        AudioManager.instance.PlaySFX(_dashingSFX, transform);
        if (_isHiFrameUsable)
        {
            _isHiFrame = true;
            _isHiFrameUsable = false;
        }
            
    }
    public void DoubleDash()
    {
        if (!_isDashing && _isDashUsable)
        {
            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
            _isDashing = true;
            _isDashUsable = false;
            AudioManager.instance.PlaySFX(_dashingSFX, transform);
            if (_isHiFrameUsable)
            {
                _isHiFrame = true;
                _isHiFrameUsable = false;
            }
        }
        else if (!_isDoubleDashing)
        {
            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
            _isDoubleDashing = true;
            AudioManager.instance.PlaySFX(_dashingSFX, transform);
            if (_isHiFrameUsable)
            {
                _isHiFrame = true;
                _isHiFrameUsable = false;
            }
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
                _isDashing = false;
                _dashTimer = 0;
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

    private void HiFrameCooldown()
    {
        _hiFrameTimer += Time.deltaTime;
        if (_hiFrameTimer > _hiFrameDuration)
        {
            _isHiFrame = false;
        }
        if (_hiFrameTimer > _hiFrameCooldown)
        {
            _isHiFrameUsable = true;
            _hiFrameTimer = 0;
        }
    }

    public void GroundPound()
    {
        if (IsGroundPounding || IsGrounded) return;

        IsGroundPounding = true;
        _rb.AddForce(Vector3.down * _groundPoundForce, ForceMode.Impulse);
        AudioManager.instance.PlaySFX(_groundPoundSFX, transform);
        if (_isDashing)
            _isDashing = false;
    }
    private void PushableChecker(Vector3 direction)
    {
        RaycastHit hit;
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, direction, out hit, 1.5f, _pushableLayer))
        {
            hit.transform.GetComponent<Rigidbody>().mass = 1;

            if (Physics.Raycast(hit.transform.position, direction, out hit2, 20f, _pushableLayer))
            {
                hit2.transform.GetComponent<Rigidbody>().mass = 1000f;
            }
        }
    }

    private void ChangeHp(int value)
    {
        if (value > 0)
        {
            _hp += value;
            EventManager.OnPlayerChangeHpNotHiFrame?.Invoke(value);
        }
        else if (value < 0 && !_isHiFrame)
        {
            EventManager.OnReset?.Invoke();
            EventManager.OnPlayerChangeHpNotHiFrame?.Invoke(value);
            _hp += value;
            
        }
        if (_hp <= 0)
            EventManager.OnPlayerDeath?.Invoke();
    }

  
    
}
