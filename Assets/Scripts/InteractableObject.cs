using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

[Serializable]
public class NewDictItem
{
    [SerializeField]
    public int index;

    [SerializeField]
    public string text;
}

[Serializable]
public class NewDict
{
    [SerializeField]
    NewDictItem[] dictItems;

    public Dictionary<int, string> ToDictionary()
    {
        Dictionary<int, string> newDict = new Dictionary<int, string>();

        foreach(var item in dictItems)
        {
            newDict.Add(item.index, item.text);
        }

        return newDict;
    }
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
    [SerializeField] List<Sprite> _objectSprites = new List<Sprite>(); // For 2D inspection

    [SerializeField] NewDict _serializedDict;
     private Dictionary<int, string> _readTextsDict = new Dictionary<int, string>(); // Can be read ? Dict, for each sprite index -> text

    [SerializeField] ObjectCanvasUI _canvas;

    [SerializeField] List<ButtonData> _buttonsData = new List<ButtonData>(); // Custom buttons
    
    private Outline _outline;
    private bool _isPlayerInTriggerSmall;

    public List<ButtonData> ButtonsData { get => _buttonsData; set => _buttonsData = value; }
    public Transform Prefab { get => _prefab; set => _prefab = value; }
    public List<Sprite> ObjectSprites { get => _objectSprites; set => _objectSprites = value; }
    public Dictionary<int, string> ReadTextsDict { get => _readTextsDict; set => _readTextsDict = value; }
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

        // Create dict
        ReadTextsDict = _serializedDict.ToDictionary();
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
