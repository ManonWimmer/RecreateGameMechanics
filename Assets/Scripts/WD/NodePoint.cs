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
    private bool _connected;
    [SerializeField] bool _receiver;

    [SerializeField] Side _side;

    public bool Connected { get => _connected; set => _connected = value; }
    public bool Receiver { get => _receiver; set => _receiver = value; }
    public Side Side { get => _side; set => _side = value; }

    // ----- FIELDS ----- //

    [Button]
    public void ToggleNodePointReceiver()
    {
        _receiver = !_receiver;
    }
}

