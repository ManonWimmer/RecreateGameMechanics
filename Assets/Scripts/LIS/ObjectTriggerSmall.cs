using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerSmall : MonoBehaviour
{
    // ----- FIELDS ----- //
    private InteractableObject _object;
    // ----- FIELDS ----- //

    private void Start()
    {
        _object = transform.parent.GetComponent<InteractableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _object.EnableCanvasActions();
        _object.PlayerInTriggerSmall();
    }

    private void OnTriggerExit(Collider other)
    {
        _object.PlayerNotInTriggerSmall();
        _object.EnableCanvasName();
    }
}

