using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MaterialColor))]
public class Cube : MonoBehaviour
{
    [SerializeField] private MaterialColor _materialColor;
    [SerializeField] private Timer _timer;

    [SerializeField] private ValueRange<float> _rangeDestroyingTime;

    public event Action<Cube> Releasing;

    private void OnCollisionEnter(Collision collision)
    {
        if (_materialColor.Swaped == false && collision.gameObject.TryGetComponent(out MeshCollider collider))
        {
            StartCoroutine(PerformDeathTimer());
            _materialColor.SetRandomColor();
        }
    }

    private IEnumerator PerformDeathTimer()
    {
        float lifeTime = UnityEngine.Random.Range(_rangeDestroyingTime.MinValue, _rangeDestroyingTime.MaxValue);

        yield return _timer.WaitSeconds(lifeTime);

        Releasing.Invoke(this);
    }
}