using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CW_PlayerController : MonoBehaviour
{
    // ----- FIELDS ----- //
    private CharacterController _characterController;

    [SerializeField] private Transform _playerCamera;

    private bool canMove = true;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    private bool _isRecording = false;
    private CW_CameraController _cameraController;
    // ----- FIELDS ----- //

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraController = _playerCamera.GetComponent<CW_CameraController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotation
        _characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Recording
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isRecording = !_isRecording;
            _cameraController.UpdateCameraRecState(_isRecording);
        }
    }
}
