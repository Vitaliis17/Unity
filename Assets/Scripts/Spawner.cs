using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private Pool<T> _pool;

    [SerializeField] private float _positionY;

    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;

    [SerializeField] private float _minPositionZ;
    [SerializeField] private float _maxPositionZ;

    public T Spawn()
    {
        T component = _pool.GetObject();

        float positionX = Random.Range(_minPositionX, _maxPositionX);
        float positionZ = Random.Range(_minPositionZ, _maxPositionZ);

        component.transform.position = new(positionX, _positionY, positionZ);

        return component;
    }

    public void Release(T component)
        => _pool.ReleaseObject(component);
}
