using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask[] _collisionLayers;

    public event Action CollisionLayerCollided;

    private void OnCollisionEnter(Collision collision)
    {
        LayerMask collisionLayer = 1 << collision.gameObject.layer;

        if (_collisionLayers.Any(layer => collisionLayer == layer.value))
            CollisionLayerCollided?.Invoke();
    }
}