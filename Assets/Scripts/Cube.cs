using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask[] _collisionLayers;

    public event Action CollisionLayerCollided;

    public Renderer Renderer { get; private set; }

    private void Awake()
        => Renderer = GetComponent<Renderer>();

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