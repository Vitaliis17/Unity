using UnityEngine;
using System.Collections;

public class ObjectHandler<T> : MonoBehaviour where T : Component
{

    [field: SerializeField, Min(1f)] public float Periodicity { get; }

    [field: SerializeField, Min(0f)] public float MinDestroyingTime { get; private set; }
    [field: SerializeField, Min(0f)] public float MaxDestroyingTime { get; private set; }

    [SerializeField] private Pool<T> _pool;
    [SerializeField] private Timer<T> _timer;
    [SerializeField] private Spawner _spawner;

    private void OnValidate()
    {
        if (MinDestroyingTime > MaxDestroyingTime)
            (MinDestroyingTime, MaxDestroyingTime) = (MaxDestroyingTime, MinDestroyingTime);
    }

    private void Awake()
    {
        float spawningPeriodicity = Periodicity;
        StartCoroutine(_timer.WaitConstantly(spawningPeriodicity));
    }

    private void OnEnable()
    {
        _timer.TimeExpired += GetObject;
        _timer.TranslateObject += _pool.ReleaseObject;
    }

    private void OnDisable()
    {
        _timer.TimeExpired -= GetObject;
        _timer.TranslateObject -= _pool.ReleaseObject;
    }

    private void GetObject()
    {
        T component = _pool.GetObject();
        IEnumerator deathTimer = GetDeathTimer(component);

        if (component is Cube cube)
            cube.CollisionLayerCollided += () => StartCoroutine(deathTimer);
    }

    private IEnumerator GetDeathTimer(T component)
    {
        float lifeTime = Random.Range(MinDestroyingTime, MaxDestroyingTime);

        return _timer.WaitSeconds(lifeTime, component);
    }
}
