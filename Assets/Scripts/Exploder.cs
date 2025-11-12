using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    public void Explode(List<Rigidbody> rigidbodies, Vector3 centre, float force, float radius)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.AddExplosionForce(force, centre, radius);
    }
}
