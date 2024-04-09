using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Xml;

public class ObjectInspector : MonoBehaviour, IDragHandler
{
    // ----- FIELDS ----- //
    public static ObjectInspector instance { get; private set; }

    [SerializeField] RawImage _rawImage3D;
    [SerializeField] RenderTexture _renderTexture;

    [SerializeField] Image _image2D;

    [SerializeField] GameObject _readBottomIcon;
    [SerializeField] GameObject _readPanel;
    [SerializeField] GameObject _next;
    [SerializeField] GameObject _previous;
    [SerializeField] TMP_Text _readTxt;

    private bool _canDrag;
    private bool _canRead;

    private bool _canGoNext;
    private bool _canGoPrevious;

    private InteractableObject _object;

    private int _currentInt = 0;

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

        if (_object.ObjectInspectorType == ObjectInspectorType.TwoDimension)
        {
            Vector2 nextOrPreviousDirection = InputManager.instance.GetNextOrPreviousDirection();

            if (nextOrPreviousDirection != Vector2.zero && _object.ObjectSprites.Count > 1) // Multiple sprites
            {
                Debug.Log(nextOrPreviousDirection.x);
                if (nextOrPreviousDirection.x == 1) // Next
                {
                    if (_canGoNext)
                    {
                        _currentInt++;
                        _image2D.sprite = _object.ObjectSprites[_currentInt];
                        CheckNextAndPrevious();
                        CheckRead();
                    }
                }
                else if (nextOrPreviousDirection.x == -1) // Previous
                {
                    if (_canGoPrevious)
                    {
                        _currentInt--;
                        _image2D.sprite = _object.ObjectSprites[_currentInt];
                        CheckNextAndPrevious();
                        CheckRead();
                    }
                }
            }
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

        _object = currentObject;

        _currentInt = 0;
        _canGoNext = false;
        _canGoPrevious = false;

        CheckRead();

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

            _next.SetActive(false);
            _previous.SetActive(false);
        }
        else // 2D
        {
            _rawImage3D.enabled = false;
            _image2D.enabled = true;
            _image2D.sprite = currentObject.ObjectSprites[_currentInt]; // _currentInt = 0
            _canDrag = false;

            CheckNextAndPrevious();
            CheckRead();
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

    private void CheckRead()
    {
        Debug.Log($"Count : {_object.ReadTextsDict.Count}, current int : {_currentInt}, contains key : {_object.ReadTextsDict.ContainsKey(_currentInt)}");
        if (_object.ReadTextsDict.Count > 0 && _object.ReadTextsDict.ContainsKey(_currentInt))
        {
            _readBottomIcon.SetActive(true);
            _readTxt.text = _object.ReadTextsDict[_currentInt]; 
            _canRead = true;
        }
        else
        {
            _readBottomIcon.SetActive(false);
            _canRead = false;
        }
    }

    private void CheckNextAndPrevious()
    {
        if (_object.ObjectSprites.Count > 1)
        {
            if (_currentInt == 0) // First
            {
                _next.SetActive(true);
                _previous.SetActive(false);

                _canGoNext = true;
                _canGoPrevious = false;
            }
            else if (_currentInt == _object.ObjectSprites.Count - 1) // Last
            {
                _next.SetActive(false);
                _previous.SetActive(true);

                _canGoNext = false;
                _canGoPrevious = true;
            }
            else // In between
            {
                _next.SetActive(true);
                _previous.SetActive(true);

                _canGoNext = true;
                _canGoPrevious = true;
            }
        }
        else
        {
            _next.SetActive(false);
            _previous.SetActive(false);

            _canGoNext = false;
            _canGoPrevious = false;
        }
    }
}
