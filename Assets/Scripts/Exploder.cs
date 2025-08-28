using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeClickHandler _cubeClickHandler;

    [SerializeField] private int _baseForce;
    [SerializeField] private int _baseRadius;

    public void ExplodeRigidbodies(Rigidbody[] rigidbodies, Vector3 centre, float radiusMultiplier, float[] forceCoefficients)
    {
        float[] forces = new float[forceCoefficients.Length];

        for (int i = 0; i < forces.Length; i++)
            forces[i] = _baseForce * forceCoefficients[i];

        float radius = _baseRadius * radiusMultiplier;

        for (int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i].AddExplosionForce(forces[i], centre, radius);
    }

    public float ReadRadius(float radiusMultiplier)
        => _baseRadius * radiusMultiplier;

    public float ReadForce(float forceMultiplier)
        => _baseForce * forceMultiplier;
}