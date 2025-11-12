using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Timer _timer;

    [SerializeField] private ValueRange<float> _rangeTransparencyTime;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRaduis;

    private TransparencySetter _transparencySetter;
    private Coroutine _coroutine;

    public event Action<ISpawnable> Releasing;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _transparencySetter = new(renderer);
    }

    private void OnEnable()
    {
        _timer.OnValueChanged += _transparencySetter.Set;
        _timer.TimeExpired += Explode;

        _coroutine = StartCoroutine(PerformDeathTimer());
    }

    private void OnDisable()
    {
        _timer.OnValueChanged -= _transparencySetter.Set;
        _timer.TimeExpired -= Explode;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator PerformDeathTimer()
    {
        float transparencyTime = UnityEngine.Random.Range(_rangeTransparencyTime.Min, _rangeTransparencyTime.Max);

        yield return _timer.Wait(transparencyTime);

        Explode();
        Releasing?.Invoke(this);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRaduis);
        List<Rigidbody> rigidbodies = new();

        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbodies.Add(rigidbody);
            }
        }

        _exploder.Explode(rigidbodies, transform.position, _explosionForce, _explosionRaduis);
    }
}