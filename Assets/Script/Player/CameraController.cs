using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private Transform _player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        InputManager.actionMap.PlayerInput.Visual.performed += Look;
    }
    private void OnDisable()
    {
        InputManager.actionMap.PlayerInput.Visual.performed -= Look;
    }
    
    private void Look(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        float newXRotation = transform.localEulerAngles.x - look.y * _rotationSpeed * Time.deltaTime;

        if (newXRotation > 180)
            newXRotation -= 360;
        newXRotation = Mathf.Clamp(newXRotation, -80, 80);

        transform.localEulerAngles = new Vector3(newXRotation, 0, 0);

        _player.transform.localEulerAngles = new Vector3(0, _player.transform.localEulerAngles.y + look.x * _rotationSpeed * Time.deltaTime, 0);
    }


}
