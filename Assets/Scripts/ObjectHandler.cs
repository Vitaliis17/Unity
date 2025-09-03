using UnityEngine;
using System.Collections;

public class ObjectHandler<T> : MonoBehaviour where T : Component
{
    [field: SerializeField, Min(1f)] public float PeriodicitySpawning { get; }

    [field: SerializeField, Min(0f)] public float MinDestroyingTime { get; private set; }
    [field: SerializeField, Min(0f)] public float MaxDestroyingTime { get; private set; }

    [SerializeField] private Timer<T> _timer;
    [SerializeField] private Spawner<T> _spawner;

    private void OnValidate()
    {
        if (MinDestroyingTime > MaxDestroyingTime)
            (MinDestroyingTime, MaxDestroyingTime) = (MaxDestroyingTime, MinDestroyingTime);
    }

    private void Awake()
    {
        float spawningPeriodicity = PeriodicitySpawning;
        StartCoroutine(_timer.WaitConstantly(spawningPeriodicity));
    }

    private void OnEnable()
    {
        _timer.TimeExpired += Spawn;
        _timer.TranslateObject += _spawner.Release;
    }

    private void OnDisable()
    {
        _timer.TimeExpired -= Spawn;
        _timer.TranslateObject -= _spawner.Release;
    }

    private void Spawn()
    {
        T component = _spawner.Spawn();
        IEnumerator deathTimer = GetDeathTimer(component);

        if (component is Cube cube)
        {
            cube.CollisionLayerCollided += () =>
            {
                StartCoroutine(deathTimer);
                cube.Renderer.material.color = Random.ColorHSV();
            };
        }
    }

    private IEnumerator GetDeathTimer(T component)
    {
        float lifeTime = Random.Range(MinDestroyingTime, MaxDestroyingTime);

        return _timer.WaitSeconds(lifeTime, component);
    }
}