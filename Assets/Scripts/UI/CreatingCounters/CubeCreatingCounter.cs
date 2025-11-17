using UnityEngine;

public class CubeCreatingCounter : MonoBehaviour
{
    [SerializeField] private TextCounter _counter;
    [SerializeField] private CubeSpawner _spawner;

    private void OnEnable()
        => _spawner.Creating += _counter.Add;

    private void OnDisable()
        => _spawner.Creating -= _counter.Add;
}