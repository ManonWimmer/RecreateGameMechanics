using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] Transform _orientation;
    [SerializeField] float _groundDrag;
    [SerializeField] Animator _animator;

    private bool _isWalking;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private Rigidbody _rb;
    // ----- FIELDS ----- //

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        GetInputs();

        SpeedControl();

        _rb.drag = _groundDrag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        // Calculate Movement Direction
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        if (_moveDirection == Vector3.zero && _isWalking)
        {
            Debug.Log("is walking false");
            _isWalking = false;
            _animator.SetBool("isWalking", false);
        }
        else if (_moveDirection != Vector3.zero && !_isWalking)
        {
            Debug.Log("is walking true");
            _isWalking = true;
            _animator.SetBool("isWalking", true);
        }

        _rb.AddForce(_moveDirection * _moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if (flatVelocity.magnitude > _moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _moveSpeed;
            _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
        }
    }
}
