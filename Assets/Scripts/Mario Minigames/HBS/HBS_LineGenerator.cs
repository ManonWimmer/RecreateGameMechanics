using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBS_LineGenerator : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Material maskMaterial;

    private HBS_Line _activeLine;

    [SerializeField] private HBS_Scratch _scratch;
    // ----- FIELDS ----- //


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(_linePrefab);
            _activeLine = newLine.GetComponent<HBS_Line>();
            newLine.layer = LayerMask.NameToLayer("Scratch");
        }

        if (Input.GetMouseButtonUp(0))
        {
            _activeLine = null;
        }

        if (_activeLine != null)
        {
            Vector2 mousePos = _playerCamera.ScreenToWorldPoint(Input.mousePosition);
            _activeLine.UpdateLine(mousePos);
            _scratch.AssignScreenAsMask();
        }
    }
}
