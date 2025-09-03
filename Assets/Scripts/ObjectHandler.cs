using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectHandler<T> : MonoBehaviour where T : Component
{
    [SerializeField] private Pool<T> _pool;
    [SerializeField] private Timer<T> _timer;

    private List<Coroutine> _deathTimers;

    private void Awake()
    {
        _deathTimers = new List<Coroutine>();

        StartCoroutine(_timer.WaitConstantly(2f));
    }

    private void OnEnable()
    {
        _timer.TimeExpired += GetObject;
        _timer.TranslateObject += ReleaseObject;
    }

    private void OnDisable()
    {
        _timer.TimeExpired -= GetObject;
        _timer.TranslateObject -= ReleaseObject;
    }

    private void GetObject()
    {
        T component = _pool.GetObject();
        Coroutine coroutine = StartDeathTimer(component);

        if (component is Cube cube)
            cube.CollisionLayerCollided += () => _deathTimers.Add(coroutine);
    }

    private void ReleaseObject(T component)
    {
        _pool.ReleaseObject(component);
    }

    private Coroutine StartDeathTimer(T component)
    {
        const float MinLifeTime = 2f;
        const float MaxLifeTime = 5f;

        float lifeTime = Random.Range(MinLifeTime, MaxLifeTime);

        IEnumerator enumerator = _timer.WaitSeconds(lifeTime, component);
        return StartCoroutine(enumerator);
    }
}
