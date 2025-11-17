using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    private T _prefab;
    private Transform _container;

    private ObjectPool<T> _pool;

    public event Action<T> Releasing;

    public event Action Spawned;
    public event Action Creating;
    public event Action<int> ActiveAmountChanged;

    protected void SetBase(T prefab, Transform container)
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
    {
        Creating?.Invoke();

        return Instantiate(_prefab, _container.transform);
    }

    private void Get(T component)
    {
        component.gameObject.SetActive(true);

        Spawned?.Invoke();
        ActiveAmountChanged?.Invoke(_pool.CountActive);
    }

    private void Release(T component)
    {
        component.gameObject.SetActive(false);

        ActiveAmountChanged?.Invoke(_pool.CountActive);
    }

    private void Destroy(T component)
        => Destroy(component.gameObject);
}