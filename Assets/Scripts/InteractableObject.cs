using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Canvas _canvas;
    private Outline _outline;
    // ----- FIELDS ----- //

    void Start()
    {
        // ----- Init Outline ----- //
        _outline = gameObject.AddComponent<Outline>();

        _outline.OutlineMode = Outline.Mode.OutlineAll;
        _outline.OutlineColor = Color.white;
        _outline.OutlineWidth = 5f;

        DisableOutline();
        // ----- Init Outline ----- //
    }

    void Update()
    {
        // Turn object name and arrow towards camera :
        _canvas.transform.LookAt(Camera.main.transform);
        // To do : compenser le rotate y avec le rotate z pour que le texte soit toujours bien droit
    }

    // ----- Enable / Disable Outline ----- //
    public void EnableOutline()
    {
        _outline.enabled = true;
    }

    public void DisableOutline()
    {
        _outline.enabled = false;
    }
    // ----- Enable / Disable Outline ----- //
}
