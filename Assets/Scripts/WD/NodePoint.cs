using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePoint : MonoBehaviour
{
    // ----- FIELDS ----- //
    private bool _connected;
    [SerializeField] bool _receiver;

    public bool Connected { get => _connected; set => _connected = value; }
    public bool Receiver { get => _receiver; set => _receiver = value; }

    // ----- FIELDS ----- //

    [Button]
    public void ToggleNodePointReceiver()
    {
        _receiver = !_receiver;
    }
}

