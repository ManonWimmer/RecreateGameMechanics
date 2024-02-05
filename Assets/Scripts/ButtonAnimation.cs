using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Sprite _sprite1;
    [SerializeField] Sprite _sprite2;
    [SerializeField] Sprite _sprite3;

    private Coroutine _coroutine;
    private Image _image;
    // ----- FIELDS ----- //

    private void Start()
    {
        _image = transform.GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(TextOffset());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator TextOffset()
    {
        yield return new WaitForSeconds(0.35f);
       _image.sprite = _sprite1;

        yield return new WaitForSeconds(0.35f);
        _image.sprite = _sprite2;

        yield return new WaitForSeconds(0.35f);
        _image.sprite = _sprite3;

        yield return StartCoroutine(TextOffset());
    }
}
