using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable
{
    [field: SerializeField] public float ExplosionForce { get; private set; }
    [field: SerializeField] public float ExplosionRaduis { get; private set; }
    
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Timer _timer;

    [SerializeField] private ValueRange<float> _rangeTransparencyTime;

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

        _coroutine = StartCoroutine(PerformDeathTimer());
    }

    private void OnDisable()
    {
        _timer.OnValueChanged -= _transparencySetter.Set;

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

        Releasing?.Invoke(this);
    }
}