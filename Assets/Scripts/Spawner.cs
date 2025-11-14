using UnityEngine;
using UnityEngine.Pool;
using System;

public class Spawner<T> where T : Component, ISpawnable
{
    private readonly T _prefab;
    private readonly Transform _container;

    private ObjectPool<T> _pool;

    public event Action<T> Releasing;

    public Spawner(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;
        
        _pool = new ObjectPool<T>(Create, Get, Release, Destroy);
    }

    public void Spawn(Vector3 position)
    {
        T component = _pool.Get();

        component.transform.position = position;
        component.Releasing += ReleaseComponent;
    }

    private void ReleaseComponent(ISpawnable spawnableObject)
    {
        T component = (T)spawnableObject;

        Releasing?.Invoke(component);
        component.Releasing -= ReleaseComponent;

        _pool.Release(component);
    }

    private T Create()
        => UnityEngine.Object.Instantiate(_prefab, _container.transform);

    private void Get(T component)
        => component.gameObject.SetActive(true);

    private void Release(T component)
        => component.gameObject.SetActive(false);

    private void Destroy(T component)
        => UnityEngine.Object.Destroy(component.gameObject);
}
