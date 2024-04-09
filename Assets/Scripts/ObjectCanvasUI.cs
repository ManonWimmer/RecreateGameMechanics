using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ObjectCanvasUI : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _itemName;
    [SerializeField] GameObject _actions;

    [SerializeField] Image _buttonImg_South;
    [SerializeField] Image _buttonImg_North;
    [SerializeField] Image _buttonImg_West;
    [SerializeField] Image _buttonImg_East;

    private TMP_Text _buttonTxt_South;
    private TMP_Text _buttonTxt_North;
    private TMP_Text _buttonTxt_West;
    private TMP_Text _buttonTxt_East;

    private InteractableObject _object;

    private List<Button> _allButtons = new List<Button>() { Button.South, Button.North, Button.West, Button.East };
    private Dictionary<Button, Tuple<Image, TMP_Text>> _dictButtons = new Dictionary<Button, Tuple<Image, TMP_Text>>();
    private List<Button> _objectButtons = new List<Button>();
    // ----- FIELDS ----- //

    private void Start()
    {
        _object = transform.parent.GetComponent<InteractableObject>();
        InitButtons();
    }

    private void InitButtons()
    {
        // Get texts from img parents :
        _buttonTxt_South = _buttonImg_South.GetComponentInChildren<TMP_Text>();
        _buttonTxt_North = _buttonImg_North.GetComponentInChildren<TMP_Text>();
        _buttonTxt_West = _buttonImg_West.GetComponentInChildren<TMP_Text>();
        _buttonTxt_East = _buttonImg_East.GetComponentInChildren<TMP_Text>();

        // Get interactable buttons of the object
        foreach (var b in _object.ButtonsData)
        {
            _objectButtons.Add(b.ButtonName);
        }

        // Create dict of all buttons
        _dictButtons[Button.South] = new Tuple<Image, TMP_Text>(_buttonImg_South, _buttonTxt_South);
        _dictButtons[Button.North] = new Tuple<Image, TMP_Text>(_buttonImg_North, _buttonTxt_North);
        _dictButtons[Button.West] = new Tuple<Image, TMP_Text>(_buttonImg_West, _buttonTxt_West);
        _dictButtons[Button.East] = new Tuple<Image, TMP_Text>(_buttonImg_East, _buttonTxt_East);
    }

    void Update()
    {
        // Turn object name and arrow towards camera :
        if (_canvas)
        {
            _canvas.transform.LookAt(Player.instance.PlayerCamera.transform);
        }

        // Don't rotate x
        Vector3 eulerAngles = _canvas.transform.eulerAngles;
        eulerAngles.x = 0f;
        _canvas.transform.eulerAngles = eulerAngles;

        // To do : compenser le rotate y avec le rotate z pour que le texte soit toujours bien droit
    }

    // ----- ENABLE / DISABLE CANVAS ----- //
    public void DisableCanvas()
    {
        _canvas.enabled = false;
    }

    public void EnableCanvasName()
    {
        _canvas.enabled = true;

        _itemName.SetActive(true);
        _actions.SetActive(false);
    }

    public void EnableCanvasActions()
    {
        _canvas.enabled = true;
        CustomButtons();

        _itemName.SetActive(false);
        _actions.SetActive(true);
    }
    // ----- ENABLE / DISABLE CANVAS ----- //

    private void CustomButtons()
    {
        foreach (Button b in _allButtons)
        {
            if (_objectButtons.Contains(b)) // Interactable button
            {
                _dictButtons[b].Item1.color = new Color(1f, 1f, 1f, 1f); // Img Opacity 1
                _dictButtons[b].Item2.enabled = true; // Text true
            }
            else // Not interactable button
            {
                _dictButtons[b].Item1.color = new Color(1f, 1f, 1f, 0.5f); // Img Opacity 0.5
                _dictButtons[b].Item2.enabled = false; // Text false
            }
        }
    }
}
