using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, ISpawnable
{
    [SerializeField] private RandomColorSetter _colorSetter;
    [SerializeField] private Timer _timer;

    [SerializeField] private ValueRange<float> _rangeDestroyingTime;

    public event Action<ISpawnable> Releasing;

    private void OnCollisionEnter(Collision collision)
    {
        if (_colorSetter.Swaped == false && collision.gameObject.TryGetComponent(out MeshCollider _))
        {
            StartCoroutine(PerformDeathTimer());
            _colorSetter.SetRandomColor();
        }
    }

    private IEnumerator PerformDeathTimer()
    {
        float lifeTime = UnityEngine.Random.Range(_rangeDestroyingTime.Min, _rangeDestroyingTime.Max);

        yield return _timer.WaitSeconds(lifeTime);

        Releasing.Invoke(this);
    }
}