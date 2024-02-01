using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Button
{
    South, North, West, East
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

    [SerializeField] Transform _prefab;

    [SerializeField] ObjectCanvasUI _canvas;

    [SerializeField] List<ButtonData> _buttonsData = new List<ButtonData>(); // Custom buttons
    
    private Outline _outline;
    private bool _isPlayerInTriggerSmall;

    public List<ButtonData> ButtonsData { get => _buttonsData; set => _buttonsData = value; }
    public Transform Prefab { get => _prefab; set => _prefab = value; }

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
