using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField, Range(0, 1)] private float _speed;

    private AudioSource _audioSource;

    private Timer _timer;
    private Directions _volumeDirection;

    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = 0f;

        _timer = new();
    }

    private void OnEnable()
    {
        _triggerHandler.Entered += Play;
        _triggerHandler.Exited += Stop;

        _timer.ConstantlyTimePassed += ChangeVolume;
    }

    private void OnDisable()
    {
        _triggerHandler.Entered -= Play;
        _triggerHandler.Exited -= Stop;

        _timer.ConstantlyTimePassed -= ChangeVolume;
    }

    private void Play()
    {
        _volumeDirection = Directions.Forward;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(_timer.WaitConstantly());
    }

    private void Stop()
    {
        _volumeDirection = Directions.Backward;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(_timer.WaitConstantly());
    }

    private void ChangeVolume()
    {
        const float MinVolume = 0f;
        const float MaxVolume = 1f;

        float targetVolume = 0;

        if (_volumeDirection == Directions.Forward && _audioSource.isPlaying == false)
            _audioSource.Play();

        switch (_volumeDirection)
        {
            case Directions.Forward:
                targetVolume = MaxVolume;
                break;
            case Directions.Backward:
                targetVolume = MinVolume;
                break;
        }

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);

        if (_audioSource.volume == MinVolume)
            _audioSource.Stop();
    }
}