using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] ObjectCanvasUI _canvas;

    private Outline _outline;
    private bool _isPlayerInTrigger;
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

        // Hide text and arrow
        DisableCanvas();
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

    // ----- Enable / Disable Canvas ----- //
    public void EnableCanvasName()
    {
        _canvas.EnableCanvasName();
    }

    public void EnableCanvasActions()
    {
        _canvas.EnableCanvasActions();
    }

    public void DisableCanvas()
    {
        _canvas.DisableCanvas();
    }
    // ----- Enable / Disable Canvas ----- //

    // ----- Player - Object Trigger ----- //
    public void PlayerInTriggerSmall()
    {
        _isPlayerInTrigger = true;
    }

    public void PlayerNotInTriggerSmall()
    {
        _isPlayerInTrigger = false;
    }

    public bool IsPlayerInTriggerSmall()
    {
        return _isPlayerInTrigger;
    }
    // ----- Player - Object Trigger ----- //
}
