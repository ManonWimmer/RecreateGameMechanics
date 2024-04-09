using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using System.Drawing;

public class Node : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Line Renderer")]
    [SerializeField] LineRenderer _lineRenderer;

    [Header("Points")]
    [SerializeField] Transform _center;
    [SerializeField] NodePoint _right;
    [SerializeField] NodePoint _left;
    [SerializeField] NodePoint _top;
    [SerializeField] NodePoint _bottom;

    [Header("Materials")]
    private Material _receiver;
    private Material _connected;
    private Material _disconnected;

    private Node _rightNode;
    private Node _leftNode;
    private Node _topNode;
    private Node _bottomNode;

    private List<NodePoint> _allPoints = new List<NodePoint>();
    private List<NodePoint> _activePoints = new List<NodePoint>();

    private bool _nodeConnected = false;

    private float _raycastDistance;
    private LayerMask _layerNode;

    private bool _isRotating;

    public NodePoint Right { get => _right; set => _right = value; }
    public NodePoint Left { get => _left; set => _left = value; }
    public NodePoint Top { get => _top; set => _top = value; }
    public NodePoint Bottom { get => _bottom; set => _bottom = value; }
    public bool NodeConnected { get => _nodeConnected; set => _nodeConnected = value; }

    // ----- FIELDS ----- //

    // Start is called before the first frame update
    void Start()
    {
        _receiver = NodesManager.instance.Receiver;
        _connected = NodesManager.instance.Connected;
        _disconnected = NodesManager.instance.Disconnected;

        _allPoints.Add(_right); 
        _allPoints.Add(_left); 
        _allPoints.Add(_top); 
        _allPoints.Add(_bottom);

        _layerNode = NodesManager.instance.NodeLayer;

        _raycastDistance = NodesManager.instance.RaycastDistance;

        GetSurroundingNodes();
    }

    private void GetSurroundingNodes()
    {
        // Right
        RaycastHit rightHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rightHit, _raycastDistance, _layerNode))
        {
            if (rightHit.collider.GetComponent<Node>())
            {
                Debug.Log(transform.name + " hit right node " + rightHit.collider.name);
                _rightNode = rightHit.collider.GetComponent<Node>();
            }
        }

        // Left
        RaycastHit leftHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out leftHit, _raycastDistance, _layerNode))
        {
            if (leftHit.collider.GetComponent<Node>())
            {
                Debug.Log(transform.name + " hit left node " + leftHit.collider.name);
                _leftNode = leftHit.collider.GetComponent<Node>();
            }
        }

        // Top
        RaycastHit topHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out topHit, _raycastDistance, _layerNode))
        {
            if (topHit.collider.GetComponent<Node>())
            {
                Debug.Log(transform.name + " hit top node " + topHit.collider.name);
                _topNode = topHit.collider.GetComponent<Node>();
            }
        }

        // Bottom
        RaycastHit bottomHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out bottomHit, _raycastDistance, _layerNode))
        {
            if (bottomHit.collider.GetComponent<Node>())
            {
                Debug.Log(transform.name + " hit bottom node " + bottomHit.collider.name);
                _bottomNode = bottomHit.collider.GetComponent<Node>();
            }   
        }
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
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * _raycastDistance, Color.yellow);

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
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, new Vector3(_center.position.x, _center.position.y, -0.05f));

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

        if (!_isRotating)
        {
            foreach (NodePoint point in _allPoints)
            {
                //CheckIfConnected(point);
            }
        }
            
    }

    private void ConnectToSurroundingNode(NodePoint connectedPoint)
    {
        switch(connectedPoint.Side)
        {
            case (Side.Left):
                if (_leftNode != null)
                {
                    if (_leftNode._isRotating)
                        break;

                    Debug.Log("a");

                    _leftNode.Right.Receiver = true;
                    if (_leftNode.Right.gameObject.activeSelf)
                    {  
                        _leftNode.NodeConnected = true;
                    }
                }  
                break;
            case (Side.Right):
                if (_rightNode != null)
                {
                    if (_rightNode._isRotating)
                        break;

                    Debug.Log("aa");
                    _rightNode.Left.Receiver = true;

                    if (_rightNode.Left.gameObject.activeSelf)
                    {
                        _rightNode.NodeConnected = true;
                    }    
                }
                break;
            case (Side.Top):
                if (_topNode != null)
                {
                    if (_topNode._isRotating)
                        break;

                    Debug.Log("aaa");
                    _topNode.Bottom.Receiver = true;

                    if (_topNode.Bottom.gameObject.activeSelf)
                    {
                        _topNode.NodeConnected = true;
                    }   
                }
                break;
            case (Side.Bottom):
                if (_bottomNode != null)
                {
                    if (_bottomNode._isRotating)
                        break;

                    Debug.Log("aaaa");
                    _bottomNode.Top.Receiver = true;

                    if (_bottomNode.Top.gameObject.activeSelf)
                    {
                        _bottomNode.NodeConnected = true;
                    }
                }
                break;
        }
    }

    private void ConnectAllNodePoints()
    {
        Debug.Log("connect all node point");
        foreach (NodePoint point in _allPoints)
        {
            if (!point.gameObject.activeSelf)
                continue;
            
            point.Connected = true;
            if (!point.Receiver)
            {
                Debug.Log("la : " + point.name);
                point.GetComponent<MeshRenderer>().material = _connected;
                ConnectToSurroundingNode(point);
            }
        }

        _lineRenderer.material = _connected;
    }

    private void CheckIfConnected(NodePoint point)
    {
        switch (point.Side)
        {
            case (Side.Left):
                if (_leftNode != null)
                {
                    Debug.Log("a");

                    if (_leftNode.Right.gameObject.activeSelf && _leftNode.Top.Connected)
                    {
                        point.Connected = true;
                    }
                    else
                    {
                        point.Connected = false;
                    }
                }
                else
                {
                    point.Connected = false;
                }
                break;
            case (Side.Right):
                if (_rightNode != null)
                {
                    Debug.Log("aa");

                    if (_rightNode.Left.gameObject.activeSelf && _rightNode.Top.Connected)
                    {
                        point.Connected = true;
                    }
                    else
                    {
                        point.Connected = false;
                    }
                }
                else
                {
                    point.Connected = false;
                }
                break;
            case (Side.Top):
                if (_topNode != null)
                {
                    Debug.Log("aaa");

                    if (_topNode.Bottom.gameObject.activeSelf && _topNode.Top.Connected)
                    {
                        point.Connected = true;
                    }
                    else
                    {
                        point.Connected = false;
                    }
                }
                else
                {
                    point.Connected = false;
                }
                break;
            case (Side.Bottom):
                if (_bottomNode != null)
                {
                    Debug.Log("aaaa");

                    if (_bottomNode.Top.gameObject.activeSelf && _bottomNode.Top.Connected)
                    {
                        point.Connected = true;
                    }
                    else
                    {
                        point.Connected = false;
                    }
                }
                else
                {
                    point.Connected = false;
                }
                break;
        }
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

    #region Toggle Sides

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

    #endregion

    [Button]
    public void RotateRightInGame()
    {
        if (!_isRotating)
        {
            StartCoroutine(RotateCoroutine(-90, true));
        }
    }

    [Button]
    public void RotateLeftInGame()
    {
        if (!_isRotating)
        {
            StartCoroutine(RotateCoroutine(90, false));
        }
    }

    IEnumerator RotateCoroutine(float degrees, bool right)
    {
        // Calculate the target rotation by adding a 90 degree rotation to the current rotation
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, degrees);

        // Rotate smoothly towards the target rotation
        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, NodesManager.instance.RotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;

        _isRotating = false;

        RotateConnectedNodes(right);
    }

    private void RotateConnectedNodes(bool right)
    {
        NodePoint tempRight = _right;
        NodePoint tempLeft = _left;
        NodePoint tempTop = _top;
        NodePoint tempBottom = _bottom;

        bool rightReceiver = false;
        bool leftReceiver = false;
        bool topReceiver = false;
        bool bottomReceiver = false;

        rightReceiver = tempRight.Receiver;
        leftReceiver = tempLeft.Receiver;
        topReceiver = tempTop.Receiver;
        bottomReceiver = tempBottom.Receiver;

        if (right)
        {
            if (rightReceiver)
            {
                _right.Receiver = false;
                _top.Receiver = true;
            }
            
            _right = tempTop;
            _top.Side = Side.Bottom;

            if (bottomReceiver)
            {
                _bottom.Receiver = false;
                _right.Receiver = true;
            }

            _bottom = tempRight;
            _right.Side = Side.Left;

            if (leftReceiver)
            {
                _left.Receiver = false;
                _bottom.Receiver = true;
            }

            _left = tempBottom;
            _bottom.Side = Side.Top;

            if (topReceiver)
            {
                _top.Receiver = false;
                _left.Receiver = true;
            }

            _top = tempLeft;
            _left.Side = Side.Right;
        }
        // TO DO : ELSE
    }
}
