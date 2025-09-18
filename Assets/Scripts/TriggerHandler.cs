using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class TriggerHandler : MonoBehaviour
{
    public Action Entered;
    public Action Exited;

    private void Awake()
        => GetComponent<Collider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Movement movement))
            Entered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Movement movement))
            Exited?.Invoke();
    }
}