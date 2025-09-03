using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MaterialColor))]
public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask[] _collisionLayers;

    public event Action CollisionLayerCollided;

    [field: SerializeField] public MaterialColor MaterialColor { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        LayerMask collisionLayer = 1 << collision.gameObject.layer;

        if (_collisionLayers.Any(layer => collisionLayer == layer.value))
        {
            CollisionLayerCollided?.Invoke();
            CollisionLayerCollided = null;
        }
    }
}