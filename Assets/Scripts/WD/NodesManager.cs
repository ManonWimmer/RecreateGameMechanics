using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static NodesManager instance;

    [Header("Materials")]
    [SerializeField] Material _receiver;
    [SerializeField] Material _connected;
    [SerializeField] Material _disconnected;

    [SerializeField] float _raycastDistance = 10f;

    [SerializeField] LayerMask _nodeLayer;

    [SerializeField] float _rotationSpeed = 90f;

    public Material Receiver { get => _receiver; set => _receiver = value; }
    public Material Connected { get => _connected; set => _connected = value; }
    public Material Disconnected { get => _disconnected; set => _disconnected = value; }
    public float RaycastDistance { get => _raycastDistance; set => _raycastDistance = value; }
    public LayerMask NodeLayer { get => _nodeLayer; set => _nodeLayer = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }
}
