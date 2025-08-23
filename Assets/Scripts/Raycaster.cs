using UnityEngine;
using System;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Camera _camera;

    [SerializeField] private LayerMask _layerMask;

    public event Action<Collider> RaycastHitting;

    private void OnEnable() 
        => _inputReader.MouseClicked += Read;

    private void OnDisable() 
        => _inputReader.MouseClicked -= Read;

    private void Read()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            RaycastHitting?.Invoke(hit.collider);
    }
}
