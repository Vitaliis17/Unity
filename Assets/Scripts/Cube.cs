using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private MaterialColor _materialColor;
    [SerializeField] private CollisionHandler _collisionHandler;

    private void OnEnable()
        => _collisionHandler.CollisionLayerCollided += _materialColor.SetRandomColor;

    private void OnDisable()
        => _collisionHandler.CollisionLayerCollided -= _materialColor.SetRandomColor;
}
