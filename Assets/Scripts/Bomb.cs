using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    private ValueRange<float> _rangeTransparencyTime;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        float transparencyTime = Random.Range(_rangeTransparencyTime.Min, _rangeTransparencyTime.Max);

        _coroutine = StartCoroutine(_timer.Wait(transparencyTime));
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}