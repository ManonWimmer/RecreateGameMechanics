using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ----- FIELDS -----//
    private InteractableObject _currentObjectOutlined;
    private bool _isOutlined = false;
    // ----- FIELDS -----//

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int objectLayer = LayerMask.GetMask("Object");


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectLayer))
        {
            Debug.Log("hit :" + hit.collider.name);
            
            if (hit.collider.TryGetComponent<InteractableObject>(out InteractableObject _object))
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
