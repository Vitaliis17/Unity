using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _directionMoving;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        Vector3 nextVelocity = _directionMoving * _speed * Time.deltaTime;
        nextVelocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = nextVelocity;
    }

    public void SetDirectionMoving(Vector3 direction)
        => _directionMoving = direction;
}