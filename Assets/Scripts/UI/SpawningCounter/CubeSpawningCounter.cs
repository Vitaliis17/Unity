using UnityEngine;

public class CubeSpawningCounter : MonoBehaviour
{
    [SerializeField] private TextCounter _counter;
    [SerializeField] private CubeSpawner _spawner;

    private void OnEnable()
        => _spawner.Spawned += _counter.Add;

    private void OnDisable()
        => _spawner.Spawned += _counter.Add;
}