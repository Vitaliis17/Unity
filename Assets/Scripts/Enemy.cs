using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float Speed;

    protected Transform CurrentPurpose;
    
    private Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _rigidbody.freezeRotation = true;
    }

    protected virtual void FixedUpdate()
        => CalculateVelocity();

    public void SetPurpose(Transform purpose)
    {
        if (CurrentPurpose != null)
            return;

        CurrentPurpose = purpose;
    }

    private void CalculateVelocity()
    {
        Vector3 directionVector = (CurrentPurpose.position - transform.position).normalized;
        Vector3 velocity = directionVector * Speed * Time.deltaTime;

        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }
}