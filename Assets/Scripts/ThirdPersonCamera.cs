using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCamera : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _player;
    [SerializeField] Transform _playerObject;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _rotationSpeed;

    [SerializeField] CinemachineFreeLook _cinemachine;
    // ----- FIELDS ----- //

    private void Update()
    {
        // Cinemachine free look
        if (UIManager.instance.IsInMenu) // Not in menu
        {
            // Stop rotating camera
            if (_cinemachine.m_XAxis.m_InputAxisName != null)
                _cinemachine.m_XAxis.m_InputAxisName = null;
            if (_cinemachine.m_YAxis.m_InputAxisName != null)
                _cinemachine.m_YAxis.m_InputAxisName = null;
        }
        else
        {
            if (_cinemachine.m_XAxis.m_InputAxisName == null)
                _cinemachine.m_XAxis.m_InputAxisName = "Mouse X";
            if (_cinemachine.m_YAxis.m_InputAxisName == null)
                _cinemachine.m_YAxis.m_InputAxisName = "Mouse Y";

            // Rotate Orientation
            Vector3 viewDirection = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
            _orientation.forward = viewDirection.normalized;

            // Rotate Player Object
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDirection = _orientation.forward * verticalInput + _orientation.right * horizontalInput;
            Vector2 test = new Vector2(horizontalInput, verticalInput);

            Debug.DrawRay(transform.position, inputDirection.normalized * 100f, Color.red);
            Debug.DrawRay(transform.position, test.normalized * 100f, Color.red);

            if (inputDirection != Vector3.zero)
            {
                _playerObject.forward = Vector3.Slerp(_playerObject.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}
