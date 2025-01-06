using UnityEngine;

public class Resizing : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    => transform.localScale += _speed * Time.deltaTime * Vector3.one;
}