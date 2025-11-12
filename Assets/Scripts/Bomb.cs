using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private ValueRange<float> _rangeTransparencyTime;

    private TransparencySetter _transparencySetter;
    private Coroutine _coroutine;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _transparencySetter = new(renderer);
    }

    private void OnEnable()
    {
        _timer.OnValueChanged += _transparencySetter.Set;
        _timer.TimeExpired += () => Destroy(gameObject);
        StartRandomTimer();
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _timer.OnValueChanged -= _transparencySetter.Set;
        _timer.TimeExpired -= () => Destroy(gameObject);
    }

    private void StartRandomTimer()
    {
        float transparencyTime = Random.Range(_rangeTransparencyTime.Min, _rangeTransparencyTime.Max);

        _coroutine = StartCoroutine(_timer.Wait(transparencyTime));
    }
}