using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCanvasUI : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _itemName;
    [SerializeField] GameObject _actions;
    // ----- FIELDS ----- //

    void Update()
    {
        // Turn object name and arrow towards camera :
        if (_canvas)
        {
            _canvas.transform.LookAt(Camera.main.transform);
        }

        // Don't rotate x
        Vector3 eulerAngles = _canvas.transform.eulerAngles;
        eulerAngles.x = 0f;
        _canvas.transform.eulerAngles = eulerAngles;

        // To do : compenser le rotate y avec le rotate z pour que le texte soit toujours bien droit
    }

    public void DisableCanvas()
    {
        _canvas.enabled = false;
    }

    public void EnableCanvasName()
    {
        _canvas.enabled = true;

        _itemName.SetActive(true);
        _actions.SetActive(false);
    }

    public void EnableCanvasActions()
    {
        _canvas.enabled = true;

        _itemName.SetActive(false);
        _actions.SetActive(true);
    }
}
