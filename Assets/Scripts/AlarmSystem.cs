using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _speed;

    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = 0f;
    }

    public void IncreaseVolume()
    {
        const float MaxValue = 1f;

        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        StartNewCorutine(ChangeVolume(MaxValue));
    }

    public void ReduceVolume()
    {
        const float MinValue = 0f;

        StartNewCorutine(ChangeVolume(MinValue));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        const float MinValue = 0f;

        WaitForFixedUpdate waitingTime = new();

        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);

            yield return waitingTime;
        }

        if (_audioSource.volume == MinValue)
            _audioSource.Stop();
    }

    private void StartNewCorutine(IEnumerator enumerator)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(enumerator);
    }
}