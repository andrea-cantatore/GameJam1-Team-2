using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraController : MonoBehaviour
{
    
    private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private Transform _player;
    
    private void Awake()
    {
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }


    private void Update()
    {
        Vector2 delta = InputManager.actionMap.PlayerInput.Visual.ReadValue<Vector2>();
        _cinemachineFreeLook.m_XAxis.Value += delta.x * _cinemachineFreeLook.m_XAxis.m_MaxSpeed * Time.deltaTime;
        _cinemachineFreeLook.m_YAxis.Value += -delta.y * _cinemachineFreeLook.m_YAxis.m_MaxSpeed * Time.deltaTime;
    }


}
