using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Left,
    Right, 
    Top, 
    Bottom,
}

public class NodePoint : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] bool _connected;
    [SerializeField] bool _receiver;

    [SerializeField] Side _side;

    [SerializeField] NodePoint _giver;

    [SerializeField] Node _node;
    public bool Connected { get => _connected; set => _connected = value; }
    public bool Receiver { get => _receiver; set => _receiver = value; }
    public Side Side { get => _side; set => _side = value; }
    public NodePoint Giver { get => _giver; set => _giver = value; }
    public Node Node { get => _node; set => _node = value; }

    // ----- FIELDS ----- //

    [Button]
    public void ToggleNodePointReceiver()
    {
        _receiver = !_receiver;
        _connected = !_connected;
    }
}

