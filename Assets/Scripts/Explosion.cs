using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private CubeClickHandler _cubeClickHandler;

    [SerializeField] private int _radius;
    [SerializeField] private int _force;

    private void OnEnable() => _cubeClickHandler.CubesExploding += Explode;

    private void OnDisable() => _cubeClickHandler.CubesExploding -= Explode;

    private void Explode(Rigidbody[] rigidbodies, Vector3 centre)
    {
        foreach(Rigidbody rigidbody in rigidbodies) 
            rigidbody.AddExplosionForce(_force, centre, _radius);
    }
}