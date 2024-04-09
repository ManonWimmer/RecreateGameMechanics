using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static UIManager instance { get; private set; }

    [SerializeField] float _textOffsetX;
    [SerializeField] float _textOffsetY;

    [SerializeField] float _textOffsetTime1;
    [SerializeField] float _textOffsetTime2;
    [SerializeField] float _textOffsetTime3;

    private bool _isInMenu;

    public float TextOffsetX { get => _textOffsetX; set => _textOffsetX = value; }
    public float TextOffsetY { get => _textOffsetY; set => _textOffsetY = value; }
    public float TextOffsetTime1 { get => _textOffsetTime1; set => _textOffsetTime1 = value; }
    public float TextOffsetTime2 { get => _textOffsetTime2; set => _textOffsetTime2 = value; }
    public float TextOffsetTime3 { get => _textOffsetTime3; set => _textOffsetTime3 = value; }
    public bool IsInMenu { get => _isInMenu; set => _isInMenu = value; }

    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (_isInMenu && (!ObjectInspectorManager.instance.IsInObjectInspectorMenu && !DialogueManager.GetInstance().DialogueIsPlaying))
        {
            _isInMenu = false;
            //CursorManagerUI.instance.ShowCursorUI();
        }
        else if (!_isInMenu && (ObjectInspectorManager.instance.IsInObjectInspectorMenu || DialogueManager.GetInstance().DialogueIsPlaying))
        {
            _isInMenu = true;
            //CursorManagerUI.instance.HideCursorUI();
        }
    }
}
