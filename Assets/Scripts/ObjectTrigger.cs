using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
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
        _object.PlayerInTrigger();
        _object.EnableCanvas();
    }

    private void OnTriggerExit(Collider other)
    {
        _object.PlayerNotInTrigger();
        _object.DisableCanvas();
    }
}

