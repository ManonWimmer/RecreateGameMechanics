using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _player;
    [SerializeField] Transform _playerObject;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _rotationSpeed;
    // ----- FIELDS ----- //

    private void Start()
    {
        // Lock & Hide Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Rotate Orientation
        Vector3 viewDirection = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDirection.normalized;

        // Rotate Player Object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        if (inputDirection != Vector3.zero)
        {
            _playerObject.forward = Vector3.Slerp(_playerObject.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
    }
}
