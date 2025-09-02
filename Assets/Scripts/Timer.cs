using System.Collections;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField, Min(0)] private float _lifeTime;

    public event Action TimeExpired;

    private IEnumerator Run()
    {
        WaitForEndOfFrame waiting = new();

        while(HasTime())
        {
            _lifeTime -= Time.deltaTime;

            yield return waiting;
        }

        TimeExpired?.Invoke();
    }

    private bool HasTime()
        => _lifeTime > 0;
}
