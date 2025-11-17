using UnityEngine;

public class BombActiveCounter : MonoBehaviour
{
    [SerializeField] private TextCounter _counter;
    [SerializeField] private BombSpawner _spawner;

    private void OnEnable()
        => _spawner.ActiveAmountChanged += _counter.Write;

    private void OnDisable()
        => _spawner.ActiveAmountChanged -= _counter.Write;
}
