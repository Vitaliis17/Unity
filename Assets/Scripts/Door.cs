using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationPoint;
    [SerializeField] private float _rotationAngle;

    [SerializeField] private TriggerHandler _triggerHandler;

    private Animator _animator;

    private void Awake()
        => _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _triggerHandler.Entered += Open;
        _triggerHandler.Exited += Close;
    }

    private void OnDisable()
    {
        _triggerHandler.Entered -= Open;
        _triggerHandler.Exited -= Close;
    }

    private void Open()
        => _animator.Play(DoorAnimationsHashes.Open);

    private void Close()
        => _animator.Play(DoorAnimationsHashes.Close);
}
