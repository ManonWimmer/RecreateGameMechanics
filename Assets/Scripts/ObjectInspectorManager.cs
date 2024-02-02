using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInspectorManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static ObjectInspectorManager instance { get; private set; }
    private bool _isInObjectInspectorMenu;
    public bool IsInObjectInspectorMenu { get => _isInObjectInspectorMenu; set => _isInObjectInspectorMenu = value; }
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideObjectInspector();
    }

    public void ShowObjectInspector()
    {
        ObjectInspector.instance.gameObject.SetActive(true);
        _isInObjectInspectorMenu = true;
    }

    public void HideObjectInspector()
    {
        ObjectInspector.instance.gameObject.SetActive(false);
        _isInObjectInspectorMenu = false;
    }
}
