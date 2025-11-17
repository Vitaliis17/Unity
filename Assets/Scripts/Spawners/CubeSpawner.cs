using UnityEngine;

public class CubeSpawner : BaseSpawner<Cube>
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;

    protected void Awake()
        => SetBase(_prefab, _container);
}