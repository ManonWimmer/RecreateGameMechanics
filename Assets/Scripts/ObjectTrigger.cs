using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    // ----- FIELDS ----- //
    private InteractableObject _object;

    private void Start()
    {
        _object = transform.parent.GetComponent<InteractableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _object.EnableCanvas();
    }

    private void OnTriggerExit(Collider other)
    {
        _object.DisableCanvas();
    }
}

