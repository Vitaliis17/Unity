using UnityEngine;

public class Patrolman : Enemy
{
    [SerializeField] private Vector3[] _purposes;

    private Vector3 _currentPurpose;

    protected override void Awake()
    {
        base.Awake();
        _currentPurpose = _purposes[0];
    }

    private void FixedUpdate()
    {
        if(IsReachePurpose())
            SetNextPurpose();

        CalculateVelocity();
    }

    private void SetNextPurpose()
    {
        int index = 0;

        for(int i = 0; i < _purposes.Length; i++)
        {
            if (_purposes[i].Equals(_currentPurpose))
            {
                index = i + 1;

                break;
            }
        }

        if (index == _purposes.Length)
            index = 0;

        _currentPurpose = _purposes[index];
    }

    private void CalculateVelocity()
    {
        Vector3 directionVector = (_currentPurpose - transform.position).normalized;
        Vector3 velocity = directionVector * _speed * Time.deltaTime;

        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }

    private bool IsReachePurpose()
    {
        const float MinDistance = 0.01f;

        Vector2 currentPosition = new(transform.position.x, transform.position.z);
        Vector2 purposePosition = new(_currentPurpose.x, _currentPurpose.z);

        return Vector2.Distance(currentPosition, purposePosition) <= MinDistance * _speed * Time.deltaTime;
    }
}