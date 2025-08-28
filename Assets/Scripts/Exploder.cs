using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeClickHandler _cubeClickHandler;

    [SerializeField] private int _baseForce;
    [SerializeField] private int _baseRadius;

    public void Explode(Rigidbody[] rigidbodies, Vector3 centre, float forceMultiplier, float radiusMultiplier)
    {
        float force = _baseForce * forceMultiplier;
        float radius = _baseRadius * radiusMultiplier;

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.AddExplosionForce(force, centre, radius);
    }

    public float ReadRadius(float radiusMultiplier = 1f)
        => _baseForce * radiusMultiplier;
}