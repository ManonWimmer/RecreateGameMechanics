using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] Sprite _sprite1;
    [SerializeField] Sprite _sprite2;
    //[SerializeField] Sprite _sprite3;

    private float _textOffsetX;
    private float _textOffsetY;

    private float _textOffsetTime1;
    private float _textOffsetTime2;
    private float _textOffsetTime3;

    private Coroutine _coroutine;
    private Image _image;
    // ----- FIELDS ----- //

    private void Start()
    {
        _textOffsetTime1 = UIManager.instance.TextOffsetTime1;
        _textOffsetTime2 = UIManager.instance.TextOffsetTime2;
        _textOffsetTime3 = UIManager.instance.TextOffsetTime3;

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
        yield return new WaitForSeconds(_textOffsetTime1);
       _image.sprite = _sprite1;

        yield return new WaitForSeconds(_textOffsetTime2);
        _image.sprite = _sprite2;

        yield return new WaitForSeconds(_textOffsetTime3);
        //_image.sprite = _sprite3;

        yield return StartCoroutine(TextOffset());
    }
}
