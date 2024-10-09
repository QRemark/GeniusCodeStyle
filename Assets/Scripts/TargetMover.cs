using UnityEngine;

[RequireComponent(typeof(Transform))]
public class TargetMover : MonoBehaviour
{
    [SerializeField] private Transform _points;

    private Transform[] _targetPoints;

    private float _moveSpeed = 1.0f;

    private int _currentPointIndex = 0;

    private void Start()
    {
        InitializeTargetPoints();
    }

    private void Update()
    {
        MoveToCurrentTarget();
    }

    private void InitializeTargetPoints()
    {
        if (_points != null)
        {
            _targetPoints = new Transform[_points.childCount];

            for (int i = 0; i < _points.childCount; i++)
                _targetPoints[i] = _points.GetChild(i);
        }
    }

    private void MoveToCurrentTarget()
    {
        Transform point = _targetPoints[_currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, point.position,
            _moveSpeed * Time.deltaTime);

        if (transform.position == point.position)
            ChangePoint();
    }

    private void ChangePoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _targetPoints.Length)
            _currentPointIndex = 0;

        Vector3 direction = _targetPoints[_currentPointIndex].transform.position - 
            transform.position;

        transform.forward = direction;
    }
}