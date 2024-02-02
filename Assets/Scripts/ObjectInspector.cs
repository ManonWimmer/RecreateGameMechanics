using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ObjectInspector : MonoBehaviour, IDragHandler
{
    // ----- FIELDS ----- //
    public static ObjectInspector instance { get; private set; }

    [SerializeField] RawImage _rawImage3D;
    [SerializeField] RenderTexture _renderTexture;

    [SerializeField] Image _image2D;

    [SerializeField] GameObject _readBottomIcon;
    [SerializeField] GameObject _readPanel;
    [SerializeField] TMP_Text _readTxt;

    private bool _canDrag;
    private bool _canRead;

    private Transform _objectPrefab;
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ClearOutRenderTexture(_renderTexture);
    }

    private void Update()
    {
        if (InputManager.instance.GetExitPressed())
        {
            ObjectInspectorManager.instance.HideObjectInspector();
        }

        if (InputManager.instance.GetSouthPressed() && _canRead)
        {
            ToggleReadPanel();
        }

        // For gamepad rotate if 3d
        if (_canDrag && InputManager.instance.GetDevice() != "Keyboard")
        {
            if (Vector3.Dot(_objectPrefab.transform.up, Vector3.up) >= 0)
            {
                _objectPrefab.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            }
            else
            {
                _objectPrefab.transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            }
        }
    }

    public void InspectObject(Transform prefab, InteractableObject currentObject)
    {
        ObjectInspectorManager.instance.ShowObjectInspector();

        HideReadPanel();

        if (currentObject.ObjectReadText != "")
        {
            _readBottomIcon.SetActive(true);
            _readTxt.text = currentObject.ObjectReadText;
            _canRead = true;
        }
        else
        {
            _readBottomIcon.SetActive(false);
            _canRead = false;
        }

        if (currentObject.ObjectInspectorType == ObjectInspectorType.ThreeDimension)
        {
            _rawImage3D.enabled = true;
            _image2D.enabled = false;
            _canDrag = true;

            if (_objectPrefab != null)
            {
                Destroy(_objectPrefab.gameObject);
            }

            _objectPrefab = Instantiate(prefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
        else // 2D
        {
            _rawImage3D.enabled = false;
            _image2D.enabled = true;
            _image2D.sprite = currentObject.ObjectSprite;
            _canDrag = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            if (Vector3.Dot(_objectPrefab.transform.up, Vector3.up) >= 0)
            {
                _objectPrefab.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x, 0);
            }
            else
            {
                _objectPrefab.transform.eulerAngles += new Vector3(eventData.delta.y, eventData.delta.x, 0);
            }
        }   
    }

    private void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;

        RenderTexture.active = renderTexture;

        GL.Clear(true, true, Color.clear);

        RenderTexture.active = rt;

    }

    public void ShowReadPanel()
    {
        _readPanel.SetActive(true);
    }

    public void HideReadPanel()
    {
        _readPanel.SetActive(false);
    }

    public void ToggleReadPanel()
    {
        _readPanel.SetActive(!_readPanel.activeSelf);
    }
}
