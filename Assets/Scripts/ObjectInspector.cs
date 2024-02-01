using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectInspector : MonoBehaviour, IDragHandler
{
    // ----- FIELDS ----- //
    public static ObjectInspector instance { get; private set; }

    private RawImage _rawImage;
    [SerializeField] RenderTexture _renderTexture;

    private Transform _objectPrefab;

    private bool _isInObjectInspectorMenu;
    public bool IsInObjectInspectorMenu { get => _isInObjectInspectorMenu; set => _isInObjectInspectorMenu = value; }
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _rawImage = GetComponentInChildren<RawImage>();
        HideObjectInspector();
        ClearOutRenderTexture(_renderTexture);
    }

    public void InspectObject(Transform prefab)
    {
        ShowObjectInspector();
        
        if (_objectPrefab != null)
        {
            Destroy(_objectPrefab.gameObject);
        }

        _objectPrefab = Instantiate(prefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
    }

    private void ShowObjectInspector()
    {
        _rawImage.enabled = true;
        _rawImage.transform.parent.gameObject.GetComponent<Image>().enabled = true; // background
        _isInObjectInspectorMenu = true;
    }

    private void HideObjectInspector()
    {
        _rawImage.enabled = false;
        _rawImage.transform.parent.gameObject.GetComponent<Image>().enabled = false; // background
        _isInObjectInspectorMenu = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Vector3.Dot(_objectPrefab.transform.up, Vector3.up) >= 0)
        {
            _objectPrefab.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x, 0);
        }
        else
        {
            _objectPrefab.transform.eulerAngles += new Vector3(eventData.delta.y, eventData.delta.x, 0);
        }
    }

    private void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;

        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.clear);

        RenderTexture.active = rt;

    }
}
