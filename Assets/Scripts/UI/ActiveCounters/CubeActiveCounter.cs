using UnityEngine;

public class CubeActiveCounter : MonoBehaviour
{
    [SerializeField] private TextCounter _counter;
    [SerializeField] private CubeSpawner _spawner;

    private void OnEnable()
        => _spawner.ActiveAmountChanged += _counter.Write;

    private void OnDisable()
        => _spawner.ActiveAmountChanged -= _counter.Write;
}
