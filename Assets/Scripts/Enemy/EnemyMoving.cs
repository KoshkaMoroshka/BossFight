using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private List<Transform> _leftStayPoints;
    [SerializeField] private List<Transform> _rightStayPoints;

    [SerializeField] private List<Transform> _leftUpperPoints;
    [SerializeField] private List<Transform> _rightUpperPoints;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeedPerSecond = 90f;
    

    [Range(0, 1)]
    private float t;
    private Transform startPoint;
    private Transform upperPoint1;
    private Transform upperPoint2;
    private Transform endPoint;
    private Transform player;

    private bool start = true;
    private bool inLastPoint = true;

    private void Start()
    {
        startPoint = _leftStayPoints[0];
        endPoint = startPoint;
        t = 0;
        player = gameObject.GetComponent<Enemy>().GetPlayerTransform();
    }

    private void Update()
    {
        if (start)
        {
            gameObject.GetComponent<Enemy>().IsAction = true;
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, _speed * Time.deltaTime);
            RotateObject();
            if (Vector3.Distance(transform.position, endPoint.position) < 1)
            {
                start = false;
                GetNewEndPoint();
            }
            return;
        }

        if (transform.position != endPoint.position)
        {
            var point = BezierDirection.GetPoint(startPoint.position, upperPoint1.position, upperPoint2.position, endPoint.position, t);
            transform.position = point;
            t += t >= 1 ? 0 : _speed * Time.deltaTime * 0.05f;
        }
        else
        {
            RotateObject();
            if (gameObject.GetComponent<Enemy>().IsAction && inLastPoint)
            {
                gameObject.GetComponent<Enemy>().IsAction = false;
                inLastPoint = false;
            }
        }
    }

    public void GetNewEndPoint()
    {
        gameObject.GetComponent<Enemy>().IsAction = true;
        inLastPoint = true;
        t = 0;
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
    private void RotateObject()
    {
        float degreesPerSecond = _rotationSpeedPerSecond * Time.deltaTime;
        Vector3 direction = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degreesPerSecond);
    }
    public bool IsStartEnemy()
    {
        return start;
    }
}
