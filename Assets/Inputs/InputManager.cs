using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 _nextOrPreviousDirection = Vector2.zero;
    private bool _northPressed = false;
    private bool _southPressed = false;
    private bool _westPressed = false;
    private bool _eastPressed = false;
    private bool _exitPressed = false;
    private bool _nextOrPreviousPressed = false;

    [SerializeField]
    private PlayerInput playerInput;

    private string _deviceUsed { get; set; }

    public static InputManager instance { get; set; }
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void NorthPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _northPressed = true;
        }
        else if (context.canceled)
        {
            _northPressed = false;
        }
    }
    public void SouthPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _southPressed = true;
        }
        else if (context.canceled)
        {
            _southPressed = false;
        }
    }
    public void WestPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _westPressed = true;
        }
        else if (context.canceled)
        {
            _westPressed = false;
        }
    }
    public void EastPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _eastPressed = true;
        }
        else if (context.canceled)
        {
            _eastPressed = false;
        }
    }

    public void ExitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _exitPressed = true;
        }
        else if (context.canceled)
        {
            _exitPressed = false;
        }
    }

    public void NextOrPreviousPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _nextOrPreviousDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _nextOrPreviousDirection = context.ReadValue<Vector2>();
        }
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    public bool GetNorthPressed()
    {
        bool result = _northPressed;
        _northPressed = false;
        return result;
    }

    public bool GetSouthPressed()
    {
        bool result = _southPressed;
        _southPressed = false;
        return result;
    }
    public bool GetWestPressed()
    {
        bool result = _westPressed;
        _westPressed = false;
        return result;
    }
    public bool GetEastPressed()
    {
        bool result = _eastPressed;
        _eastPressed = false;
        return result;
    }
    public bool GetExitPressed()
    {
        bool result = _exitPressed;
        _exitPressed = false;
        return result;
    }

    public Vector2 GetNextOrPreviousDirection()
    {
        Vector2 result = _nextOrPreviousDirection;
        if (result.x == 0)
        {
            _nextOrPreviousPressed = false;
        }
        result.y = 0;

        if (result.x > 0.5f  && !_nextOrPreviousPressed) // Right
        {
            Debug.Log("right");
            _nextOrPreviousPressed = true;
            return new Vector2(1, 0); // Right
        }
        else if (result.x < -0.5f && !_nextOrPreviousPressed) // Left
        {
            Debug.Log("left");
            _nextOrPreviousPressed = true;
            return new Vector2(-1, 0); // Left
        }

            return Vector2.zero;
    }

    private void UpdateDevice()
    {
        _deviceUsed = playerInput.currentControlScheme;
    }

    public void OnControlsChanged()
    {
        Debug.Log("controls changed");
        UpdateDevice();
    }
    public string GetDevice()
    {
        return _deviceUsed;
    }
}
