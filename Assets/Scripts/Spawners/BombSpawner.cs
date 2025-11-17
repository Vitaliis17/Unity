using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    [SerializeField] private Bomb _prefab;
    [SerializeField] private Transform _container;

    private void Awake()
        => SetBase(_prefab, _container);
}