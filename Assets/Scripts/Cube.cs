using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RandomColorSetter))]
public class Cube : MonoBehaviour
{
    [SerializeField] private RandomColorSetter _colorSetter;
    [SerializeField] private Timer _timer;

    [SerializeField] private ValueRange<float> _rangeDestroyingTime;

    public event Action<Cube> Releasing;

    private void OnCollisionEnter(Collision collision)
    {
        if (_colorSetter.Swaped == false && collision.gameObject.TryGetComponent(out MeshCollider collider))
        {
            StartCoroutine(PerformDeathTimer());
            _colorSetter.SetRandomColor();
        }
    }

    private IEnumerator PerformDeathTimer()
    {
        float lifeTime = UnityEngine.Random.Range(_rangeDestroyingTime.MinValue, _rangeDestroyingTime.MaxValue);

        yield return _timer.WaitSeconds(lifeTime);

        Releasing.Invoke(this);
    }
}