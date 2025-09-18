using UnityEngine;

[RequireComponent(typeof(AlarmSystem))]
public class Home : MonoBehaviour
{
    [SerializeField] private TriggerHandler _alarmSystemZone;

    private AlarmSystem _alarmSystem;

    private void Awake()
        => _alarmSystem = GetComponent<AlarmSystem>();

    private void OnEnable()
    {
        _alarmSystemZone.Entered += _alarmSystem.IncreaseVolume;
        _alarmSystemZone.Exited += _alarmSystem.ReduceVolume;
    }

    private void OnDisable()
    {
        _alarmSystemZone.Entered -= _alarmSystem.IncreaseVolume;
        _alarmSystemZone.Exited -= _alarmSystem.ReduceVolume;
    }
}