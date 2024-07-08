using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HBS_Line : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Vector2> _points = new List<Vector2>();
    // ----- FIELDS ----- //

    public void UpdateLine(Vector2 position)
    {
        if (_points == null || _points.Count < 1)
        {
            SetPoint(position);
            return;
        }

        SetPoint(position);
        /*
        if (Vector2.Distance(_points[_points.Count - 1], position) > .05f)
        {
            SetPoint(position);
        }
        */
    }

    private void SetPoint(Vector2 point)
    {
        Debug.Log("set point " + point);
        _points.Add(point);

        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPosition(_points.Count - 1, point);

        /*
        if (_points.Count % 10 == 0)
        {
            _lineRenderer.Simplify(.1f);
        }*/

        _points.Clear();
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _points.Add((Vector2)_lineRenderer.GetPosition(i));
        }
    }
}
