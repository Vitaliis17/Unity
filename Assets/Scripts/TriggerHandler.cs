using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class TriggerHandler : MonoBehaviour
{
    public event Action Entered;
    public event Action Exited;

    private void Awake()
        => GetComponent<Collider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Mover movement))
            Entered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Mover movement))
            Exited?.Invoke();
    }
}