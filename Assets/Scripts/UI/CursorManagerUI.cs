using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagerUI : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static CursorManagerUI instance { get; private set; }

    [SerializeField] GameObject _canvasCusor;
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    public void ShowCursorUI()
    {
        _canvasCusor.SetActive(true);
    }

    public void HideCursorUI()
    {
        _canvasCusor.SetActive(false);
    }
}
