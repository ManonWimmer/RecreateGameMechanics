using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WD_Player : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] float cameraSpeed = 0.1f; 
    [SerializeField] float raycastDistance = 100f;
    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] Camera _playerCamera;
    [SerializeField] LayerMask _nodeLayer;

    private Node _currentNodeOutlined;
    private bool _isOutlined = false;
    // ----- FIELDS ----- //


    void Update()
    {
        // Get the position of the mouse on the screen
        Vector3 mousePosition = Input.mousePosition;
        // Calculate the direction from the camera to the mouse position
        Vector3 targetDirection = _playerCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _playerCamera.transform.position.y)) - _playerCamera.transform.position;
        // Calculate the rotation quaternion to look towards the mouse position
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        // Smoothly rotate the camera towards the mouse position
        _playerCamera.transform.rotation = Quaternion.Slerp(_playerCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Cast a ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, raycastDistance, _nodeLayer))
        {
            if (hit.collider.TryGetComponent<Node>(out Node _node))
            {
                if (_currentNodeOutlined != null)
                {
                    if (_currentNodeOutlined != _node) // Change current object
                    {
                        _currentNodeOutlined.DisableOutline();
                        _currentNodeOutlined = _node;
                        _node.EnableOutline();
                    }
                    // Else -> same object hit, we change nothing (already outlined)
                }
                else // New object
                {
                    _currentNodeOutlined = _node;
                    _node.EnableOutline();
                }
                _isOutlined = true;
            }
        }
        else // No hit
        {
            if (_isOutlined)
            {
                _currentNodeOutlined.DisableOutline();
                _currentNodeOutlined = null;
                _isOutlined = false;
            }
        }

        // Check if mouse button is pressed
        if (Input.GetMouseButtonUp(0) && _currentNodeOutlined != null)
        {
            _currentNodeOutlined.RotateRightInGame();
        }
    }
}
