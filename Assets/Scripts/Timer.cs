using System.Collections;
using UnityEngine;
using System;

public class Timer<T> : MonoBehaviour where T : Component
{
    public event Action TimeExpired;
    public event Action<T> TranslateObject;

    public IEnumerator WaitSeconds(float waitingTime, T component)
    {
        yield return new WaitForSeconds(waitingTime);

        TranslateObject?.Invoke(component);
    }

    public IEnumerator WaitConstantly(float waitingTime)
    {
        WaitForSeconds waiting = new(waitingTime);

        while (true)
        {
            TimeExpired?.Invoke();

            yield return waiting;
        }
    }
}
