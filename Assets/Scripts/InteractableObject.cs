using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Button
{
    South, North, West, East
}

[Serializable]
public enum ObjectInspectorType
{
    ThreeDimension, TwoDimension, TargetCamera
}

public class InteractableObject : MonoBehaviour
{
    // ----- FIELDS ----- //
    [System.Serializable]
    public class ButtonData
    {
        public Button ButtonName;
        public String ActionName;
    }

    [SerializeField] ObjectInspectorType _objectInspectorType;

    [SerializeField] Transform _prefab; // For 3D inspection
    [SerializeField] Sprite _objectSprite; // For 2D inspection
    [SerializeField] String _objectReadText; // Can be read ? string null or not

    [SerializeField] ObjectCanvasUI _canvas;

    [SerializeField] List<ButtonData> _buttonsData = new List<ButtonData>(); // Custom buttons
    
    private Outline _outline;
    private bool _isPlayerInTriggerSmall;

    public List<ButtonData> ButtonsData { get => _buttonsData; set => _buttonsData = value; }
    public Transform Prefab { get => _prefab; set => _prefab = value; }
    public Sprite ObjectSprite { get => _objectSprite; set => _objectSprite = value; }
    public string ObjectReadText { get => _objectReadText; set => _objectReadText = value; }
    public ObjectInspectorType ObjectInspectorType { get => _objectInspectorType; set => _objectInspectorType = value; }

    // ----- FIELDS ----- //

    void Start()
    {
        // ----- Init Outline ----- //
        _outline = gameObject.AddComponent<Outline>();

        _outline.OutlineMode = Outline.Mode.OutlineAll;
        _outline.OutlineColor = Color.white;
        _outline.OutlineWidth = 5f;

        DisableOutline();
        // ----- Init Outline ----- //

        // Hide text and arrow
        DisableCanvas();
    }

    // ----- Enable / Disable Outline ----- //
    public void EnableOutline()
    {
        _outline.enabled = true;
    }

    public void DisableOutline()
    {
        _outline.enabled = false;
    }
    // ----- Enable / Disable Outline ----- //

    // ----- Enable / Disable Canvas ----- //
    public void EnableCanvasName()
    {
        _canvas.EnableCanvasName();
    }

    public void EnableCanvasActions()
    {
        _canvas.EnableCanvasActions();
    }

    public void DisableCanvas()
    {
        _canvas.DisableCanvas();
    }
    // ----- Enable / Disable Canvas ----- //

    // ----- Player - Object Trigger ----- //
    public void PlayerInTriggerSmall()
    {
        _isPlayerInTriggerSmall = true;
    }

    public void PlayerNotInTriggerSmall()
    {
        _isPlayerInTriggerSmall = false;
    }

    public bool IsPlayerInTriggerSmall()
    {
        return _isPlayerInTriggerSmall;
    }
    // ----- Player - Object Trigger ----- //
}
