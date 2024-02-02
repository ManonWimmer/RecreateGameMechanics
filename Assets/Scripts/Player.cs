using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using static InteractableObject;

public class Player : MonoBehaviour
{
    // ----- FIELDS -----//
    [SerializeField] LayerMask _interactableLayer;
    private InteractableObject _currentObjectOutlined;
    private bool _isOutlined = false;
    // ----- FIELDS -----//

    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //-> Not working with gamepad
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); // Center of the screen
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _interactableLayer) && !UIManager.instance.IsInMenu)
        {
            //Debug.DrawLine(transform.position, hit.point, Color.blue);
            //Debug.Log("hit :" + hit.collider.name);
            
            if (hit.collider.TryGetComponent<InteractableObject>(out InteractableObject _object))
            {
                if (_object.IsPlayerInTriggerSmall()) // Player in trigger -> can outline
                {
                    if (_currentObjectOutlined != null)
                    {
                        if (_currentObjectOutlined != _object) // Change current object
                        {
                            _currentObjectOutlined.DisableOutline();
                            _currentObjectOutlined = _object;
                            _object.EnableOutline();
                        }
                        // Else -> same object hit, we change nothing (already outlined)
                    }
                    else // New object
                    {
                        _currentObjectOutlined = _object;
                        _object.EnableOutline();
                    }
                    _isOutlined = true;

                    // Inputs
                    if (_currentObjectOutlined.ButtonsData.Count > 0)
                    {
                        foreach (var b in _currentObjectOutlined.ButtonsData)
                        {
                            switch (b.ButtonName)
                            {
                                case Button.North:
                                    if (InputManager.instance.GetNorthPressed())
                                    { 
                                        if (_currentObjectOutlined.ObjectInspectorType != ObjectInspectorType.TargetCamera)
                                        {
                                            ObjectInspector.instance.InspectObject(_currentObjectOutlined.Prefab, _currentObjectOutlined); // 2D or 3D
                                        }
                                        // todo: else -> inspection target camera
                                    }
                                    break;
                            }
                        }
                    }

                } 
                else // Player not in trigger -> can't outline
                {
                    if (_isOutlined)
                    {
                        _currentObjectOutlined.DisableOutline();
                        _currentObjectOutlined = null;
                        _isOutlined = false;
                    }
                }
            }
        }
        else // No hit
        {
            if (_isOutlined)
            {
                _currentObjectOutlined.DisableOutline();
                _currentObjectOutlined = null;
                _isOutlined = false;
            }   
        }
    }
}
