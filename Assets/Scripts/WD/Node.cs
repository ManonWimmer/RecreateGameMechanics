using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class Node : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Line Renderer")]
    [SerializeField] LineRenderer _lineRenderer;

    [Header("Points")]
    [SerializeField] NodePoint _center;
    [SerializeField] NodePoint _right;
    [SerializeField] NodePoint _left;
    [SerializeField] NodePoint _top;
    [SerializeField] NodePoint _bottom;

    [Header("Materials")]
    [SerializeField] Material _receiver;
    [SerializeField] Material _connected;
    [SerializeField] Material _disconnected;

    private List<NodePoint> _allPoints = new List<NodePoint>();
    private List<NodePoint> _activePoints = new List<NodePoint>();

    private bool _nodeConnected = false;
    // ----- FIELDS ----- //

    // Start is called before the first frame update
    void Start()
    {
        _allPoints.Add(_right); 
        _allPoints.Add(_left); 
        _allPoints.Add(_top); 
        _allPoints.Add(_bottom); 
    }

    private void GetActivePoints()
    {
        if (_activePoints.Count > 0) 
            _activePoints.Clear();

        foreach (NodePoint point in _allPoints)
        {
            if (point.gameObject.activeSelf)
                _activePoints.Add(point);
        }
    }

    private void Update()
    {
        _lineRenderer.ResetBounds();
        _lineRenderer.positionCount = 0;

        _nodeConnected = false;

        foreach (NodePoint point in _allPoints)
        {
            if (point.gameObject.activeSelf)
            {
                // Trait point -> center
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, new Vector3(point.transform.position.x, point.transform.position.y, -.05f));
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, new Vector3(_center.transform.position.x, _center.transform.position.y, -0.05f));

                if (point.Receiver)
                {
                    _nodeConnected = true;
                    point.GetComponent<MeshRenderer>().material = _receiver;
                }
            }
        }

        if (_nodeConnected)
            ConnectAllNodePoints();
        else
            DisconnectAllNodePoints();
    }

    private void ConnectAllNodePoints()
    {
        foreach (NodePoint point in _allPoints)
        {
            point.Connected = true;
            if (!point.Receiver)
                point.GetComponent<MeshRenderer>().material = _connected;
        }

        _lineRenderer.material = _connected;
    }

    private void DisconnectAllNodePoints()
    {
        foreach (NodePoint point in _allPoints)
        {
            point.Connected = false;
            point.GetComponent<MeshRenderer>().material = _disconnected;
        }

        _lineRenderer.material = _disconnected;
    }


    [Button]
    public void ToggleRight()
    {
        _right.gameObject.SetActive(!_right.gameObject.activeSelf);
        GetActivePoints();
    }

    [Button]
    public void ToggleLeft()
    {
        _left.gameObject.SetActive(!_left.gameObject.activeSelf);
        GetActivePoints();
    }

    [Button]
    public void ToggleTop()
    {
        _top.gameObject.SetActive(!_top.gameObject.activeSelf);
        GetActivePoints();
    }

    [Button]
    public void ToggleBottom()
    {
        _bottom.gameObject.SetActive(!_bottom.gameObject.activeSelf);
        GetActivePoints();
    }
}
