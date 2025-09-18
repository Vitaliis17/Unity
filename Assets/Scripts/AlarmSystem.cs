using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    private const float _maxVolume = 1f;
    private const float _minVolume = 0f;

    [SerializeField, Range(0, 1)] private float _speed;

    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = _minVolume;
    }

    public void IncreaseVolume()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        StartNewCorutine(ChangeVolume(_maxVolume));
    }

    public void ReduceVolume()
        => StartNewCorutine(ChangeVolume(_minVolume));

    private IEnumerator ChangeVolume(float targetVolume)
    {
        WaitForFixedUpdate waitingTime = new();

        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.fixedDeltaTime);

            yield return waitingTime;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }

    private void StartNewCorutine(IEnumerator enumerator)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(enumerator);
    }
}