using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    // ----- FIELDS ----- //
    private float _textOffsetX;
    private float _textOffsetY;

    private float _textOffsetTime1;
    private float _textOffsetTime2;
    private float _textOffsetTime3;

    private Coroutine _coroutine;
    private RectTransform _rect;
    private Vector3 _startPosition;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (gameObject.layer == 5) // UI
        {
            _textOffsetX = UIManager.instance.TextOffsetX * 10f;
            _textOffsetY = UIManager.instance.TextOffsetY * 10f;
        }
        else // Not UI
        {
            _textOffsetX = UIManager.instance.TextOffsetX;
            _textOffsetY = UIManager.instance.TextOffsetY;
        }   

        _textOffsetTime1 = UIManager.instance.TextOffsetTime1;
        _textOffsetTime2 = UIManager.instance.TextOffsetTime2;
        _textOffsetTime3 = UIManager.instance.TextOffsetTime3;

        _rect = transform.GetComponent<RectTransform>();

        _startPosition = _rect.localPosition;
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
        yield return new WaitForSeconds(_textOffsetTime1 + Random.Range(0, 0.5f));
        _rect.localPosition = new Vector3(_startPosition.x + _textOffsetX, _startPosition.y, _startPosition.z);

        yield return new WaitForSeconds(_textOffsetTime2 + Random.Range(0, 0.5f));
        _rect.localPosition = new Vector3(_startPosition.x + _textOffsetX / 2, _startPosition.y + _textOffsetY, _startPosition.z);

        yield return new WaitForSeconds(_textOffsetTime3 + Random.Range(0, 0.5f));
        _rect.localPosition = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z);

        yield return StartCoroutine(TextOffset());
    }
}
