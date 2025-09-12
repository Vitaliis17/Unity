using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float Speed;

    private Rigidbody _rigidbody;

    protected Transform CurrentPurpose;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
        => CalculateVelocity();

    private void CalculateVelocity()
    {
        Vector3 directionVector = (CurrentPurpose.position - transform.position).normalized;
        Vector3 velocity = directionVector * Speed * Time.deltaTime;

        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }
}