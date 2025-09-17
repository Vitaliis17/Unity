using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField, Range(0, 1)] private float _speed;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = 0f;
    }

    private void OnEnable()
    {
        _triggerHandler.Entered += IncreaseVolume;
        _triggerHandler.Exited += ReduceVolume;
    }

    private void OnDisable()
    {
        _triggerHandler.Entered -= IncreaseVolume;
        _triggerHandler.Exited -= ReduceVolume;
    }

    private void IncreaseVolume()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        float maxValue = (int)VolumeValues.Max;
        StartNewCorutine(ChangeVolume(maxValue));
    }

    private void ReduceVolume()
    {
        float minValue = (int)VolumeValues.Min;
        StartNewCorutine(ChangeVolume(minValue));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        WaitForFixedUpdate waitingTime = new();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);

            yield return waitingTime;
        }

        if (_audioSource.volume == (int)VolumeValues.Min)
            _audioSource.Stop();
    }

    private void StartNewCorutine(IEnumerator enumerator)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(enumerator);
    }
}