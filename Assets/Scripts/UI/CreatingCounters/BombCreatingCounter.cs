using UnityEngine;

public class BombCreatingCounter : MonoBehaviour
{
    [SerializeField] private TextCounter _counter;
    [SerializeField] private BombSpawner _spawner;

    private void OnEnable()
        => _spawner.Creating += _counter.Add;

    private void OnDisable()
        => _spawner.Creating -= _counter.Add;
}