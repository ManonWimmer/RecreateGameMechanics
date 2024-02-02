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
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _rawImage = GetComponentInChildren<RawImage>();
        ClearOutRenderTexture(_renderTexture);
    }

    public void InspectObject(Transform prefab)
    {
        ObjectInspectorManager.instance.ShowObjectInspector();
        
        if (_objectPrefab != null)
        {
            Destroy(_objectPrefab.gameObject);
        }

        _objectPrefab = Instantiate(prefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
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
