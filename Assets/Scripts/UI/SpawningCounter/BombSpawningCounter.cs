using UnityEngine;

public class BombSpawningCounter : MonoBehaviour
{
    [SerializeField] private TextCounter _counter;
    [SerializeField] private BombSpawner _spawner;

    private void OnEnable()
        => _spawner.Spawned += _counter.Add;

    private void OnDisable()
        => _spawner.Spawned += _counter.Add;
}