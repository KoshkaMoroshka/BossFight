using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private List<Transform> _leftStayPoints;
    [SerializeField] private List<Transform> _rightStayPoints;

    [SerializeField] private List<Transform> _leftUpperPoints;
    [SerializeField] private List<Transform> _rightUpperPoints;

    [Range(0, 1)]
    private float t;
    private Transform startPoint;
    private Transform upperPoint1;
    private Transform upperPoint2;
    private Transform endPoint;

    private bool start = true;

    private void Start()
    {
        startPoint = _leftStayPoints[0];
        endPoint = startPoint;
        t = 0;
    }

    private void Update()
    {
        if (start)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, 2f);
            if (Vector3.Distance(transform.position, endPoint.position) < 1)
            {
                start = false;
                GetNewPoint();
            }
            return;
        }

        if (transform.position != endPoint.position)
        {
            var point = BezierDirection.GetPoint(startPoint.position, upperPoint1.position, upperPoint2.position, endPoint.position, t);
            transform.position = point;
            transform.rotation = Quaternion.LookRotation(point);
            t += t >= 1 ? 0 : 0.1f * Time.deltaTime;
            if (Vector3.Distance(transform.position, endPoint.position) < 0.1)
            {
                t = 0;
                GetNewPoint();
            }
        }
    }

    private void GetNewPoint()
    {
        startPoint = endPoint;
        endPoint = GetRandomNewEndPoint(endPoint);
        UpdateUpperPoints(startPoint);
    }
    private void UpdateUpperPoints(Transform startPoint)
    {
        if (_leftStayPoints.Contains(startPoint))
        {
            upperPoint1 = _leftUpperPoints[Random.Range(0, 1)];
            upperPoint2 = _rightUpperPoints[Random.Range(0, 1)];
        }
        else
        {
            upperPoint1 = _rightUpperPoints[Random.Range(0, 1)];
            upperPoint2 = _leftUpperPoints[Random.Range(0, 1)];
        }
    }
    private Transform GetRandomNewEndPoint(Transform endPoint)
    {
        if (_leftStayPoints.Contains(endPoint))
            return _rightStayPoints[Random.Range(0, _rightStayPoints.Count - 1)];
        else
            return _leftStayPoints[Random.Range(0, _rightStayPoints.Count - 1)];
    }
}
