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

    [SerializeField] bool _canTurn = true;

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

        foreach(NodePoint point in _allPoints)
        {
            point.Node = this;
        }

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
                    CheckIfGiverConnected(point);

                    if (point.Connected)
                    {
                        _nodeConnected = true;
                        point.GetComponent<MeshRenderer>().material = _receiver;
                    }
                }
            }
        }

        if (_nodeConnected)
            ConnectAllNodePoints();
        else
            DisconnectAllNodePoints();
            
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

                    if (_leftNode.Right.gameObject.activeSelf)
                    {
                        _leftNode.Right.Receiver = true;
                        _leftNode.Right.Giver = connectedPoint;
                        _leftNode.Right.Connected = true;
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
                    
                    if (_rightNode.Left.gameObject.activeSelf)
                    {
                        _rightNode.Left.Receiver = true;
                        _rightNode.Left.Giver = connectedPoint;
                        _rightNode.Left.Connected = true;
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
                    
                    if (_topNode.Bottom.gameObject.activeSelf)
                    {
                        _topNode.Bottom.Receiver = true;
                        _topNode.Bottom.Giver = connectedPoint;
                        _topNode.Bottom.Connected = true;
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
                    
                    if (_bottomNode.Top.gameObject.activeSelf)
                    {
                        _bottomNode.Top.Receiver = true;
                        _bottomNode.Top.Giver = connectedPoint;
                        _bottomNode.Top.Connected = true;
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

    private void CheckIfGiverConnected(NodePoint point)
    {
        Debug.Log("check giver " + point.name);
        if (point.Giver == null)
            return;

        Debug.Log(point.Giver.Side + " " + point.Giver.Node.name + " " + point.Side);

        if (point.Giver.Side == Side.Left && _rightNode == point.Giver.Node && point.Side == Side.Right)
        {
            Debug.Log("ici");
            point.Connected = true;
        } 
        else if (point.Giver.Side == Side.Right && _leftNode == point.Giver.Node && point.Side == Side.Left)
        {
            Debug.Log("ici a");
            point.Connected = true;
        }
        else if (point.Giver.Side == Side.Bottom && _topNode == point.Giver.Node && point.Side == Side.Top)
        {
            Debug.Log("ici aa");
            point.Connected = true;
        }
        else if (point.Giver.Side == Side.Top && _bottomNode == point.Giver.Node && point.Side == Side.Bottom)
        {
            Debug.Log("ici aaa");
            point.Connected = true;
        }
        else
        {
            Debug.Log("disconnect");
            DisconnectPoint(point);
        }
    }

    private void DisconnectAllNodePoints()
    {
        foreach (NodePoint point in _allPoints)
        {
            DisconnectPoint(point);
        }

        _lineRenderer.material = _disconnected;
    }

    private void DisconnectPoint(NodePoint point)
    {
        point.Connected = false;
        point.GetComponent<MeshRenderer>().material = _disconnected;
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
        if (!_isRotating && _canTurn)
        {
            StartCoroutine(RotateCoroutine(-90, true));
        }
    }

    [Button]
    public void RotateLeftInGame()
    {
        if (!_isRotating && _canTurn)
        {
            StartCoroutine(RotateCoroutine(90, false));
        }
    }

    IEnumerator RotateCoroutine(float degrees, bool right)
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, degrees);

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

        bool rightReceiver = tempRight.Receiver;
        bool leftReceiver = tempLeft.Receiver;
        bool topReceiver = tempTop.Receiver;
        bool bottomReceiver = tempBottom.Receiver;

        Debug.Log("right receiver : " + rightReceiver);

        NodePoint rightGiver = tempRight.Giver;
        NodePoint leftGiver = tempLeft.Giver;
        NodePoint topGiver = tempTop.Giver;
        NodePoint bottomGiver = tempBottom.Giver;

        if (right)
        {
            _bottom.Side = Side.Left;
            _left.Side = Side.Top;
            _right.Side = Side.Bottom;
            _top.Side = Side.Right;

            

            if (rightReceiver)
            {
                _right.Receiver = false;
                _right.Giver = null;
                _top.Receiver = true;
                _top.Giver = rightGiver;
            }

            if (bottomReceiver)
            {
                _bottom.Receiver = false;
                _bottom.Giver = null;
                _right.Receiver = true;
                _right.Giver = bottomGiver;

            }

            if (leftReceiver)
            {
                _left.Receiver = false;
                _left.Giver = null;
                _bottom.Receiver = true;
                _bottom.Giver = leftGiver;
            }

            if (topReceiver)
            {
                _top.Receiver = false;
                _top.Giver = null;
                _left.Receiver = true;
                _left.Giver = topGiver;
            }

            _right = tempTop;
            _bottom = tempRight;
            _left = tempBottom;
            _top = tempLeft;
        }
        else
        {
            _bottom.Side = Side.Right;
            _left.Side = Side.Bottom;
            _right.Side = Side.Top;
            _top.Side = Side.Left;

            if (rightReceiver)
            {
                _right.Receiver = false;
                _right.Giver = null;
                _bottom.Receiver = true;
                _bottom.Giver = rightGiver;
            }

            _right = tempBottom;

            if (bottomReceiver)
            {
                _bottom.Receiver = false;
                _bottom.Giver = null;
                _left.Receiver = true;
                _left.Giver = bottomGiver;

            }

            _bottom = tempLeft;

            if (leftReceiver)
            {
                _left.Receiver = false;
                _left.Giver = null;
                _top.Receiver = true;
                _top.Giver = leftGiver;
            }

            _left = tempTop;

            if (topReceiver)
            {
                _top.Receiver = false;
                _top.Giver = null;
                _right.Receiver = true;
                _right.Giver = topGiver;
            }

            _top = tempRight;
        }
    }
}
