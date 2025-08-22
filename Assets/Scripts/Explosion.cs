using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int _radius;
    [SerializeField] private int _force;

    public event Func<List<Rigidbody>> GameobjectExploding;

    private void OnMouseDown()
    {
        List<Rigidbody> rigidbodies = GameobjectExploding?.Invoke();

        if(rigidbodies != null)
        {
            for(int i = 0; i < rigidbodies.Count; i++)
            {
                rigidbodies[i].AddExplosionForce(_force, transform.position, _radius);
            }
        }

        Destroy(gameObject);
    }
}