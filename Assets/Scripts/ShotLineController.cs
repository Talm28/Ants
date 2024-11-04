using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLineController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform[] _points = new Transform[2];

    void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPoints(Transform source, Transform target)
    {
        _points[0] = source;
        _points[1] = target;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _points[0].position);
        _lineRenderer.SetPosition(1, _points[1].position);
    }
}
