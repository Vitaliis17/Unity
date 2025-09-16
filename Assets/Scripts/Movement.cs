using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform[] _movingPoints;

    [SerializeField] private float _speed;

    private Transform _currentMovingPoint;
    private Rigidbody _rigidbody;

    private int _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _direction = (int)Directions.Forward;

        _currentMovingPoint = _movingPoints[0];
    }

    private void FixedUpdate()
    {
        float maxDistanceDelta = _speed * Time.deltaTime;
        Vector3 nextPosition = Vector3.MoveTowards(_rigidbody.position, _currentMovingPoint.position, maxDistanceDelta);

        _rigidbody.MovePosition(nextPosition);

        if(IsReachedPoint())
            SetNextCurrentMovingPoint();
    }

    private bool IsReachedPoint()
        => _rigidbody.position == _currentMovingPoint.position;

    private void SetNextCurrentMovingPoint()
    {
        const int FirstIndex = 0;

        int currentIndex = GetCurrentMovingPointIndex();
        int nextIndex = currentIndex + _direction;

        if(nextIndex == FirstIndex || nextIndex == _movingPoints.Length - 1)
            ReverseDirection();

        _currentMovingPoint = _movingPoints[nextIndex];
    }

    private void ReverseDirection()
    {
        if(_direction == (int)Directions.Forward)
        {
            _direction = (int)Directions.Backward;
            return;
        }

        _direction = (int)Directions.Forward;
    }

    private int GetCurrentMovingPointIndex()
    {
        int index = 0;

        for(int i = 0; i < _movingPoints.Length; i++)
        {
            if(_movingPoints[i] == _currentMovingPoint)
            {
                index = i;
                break;
            }
        }

        return index;
    }
}