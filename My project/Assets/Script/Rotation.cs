using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
        => transform.Rotate(_speed * Time.deltaTime * Vector3.up);
}