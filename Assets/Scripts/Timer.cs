using System.Collections;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action TimeExpired;
    public event Action<Cube> TranslateObject;

    public IEnumerator WaitSeconds(float waitingTime, Cube cube)
    {
        yield return new WaitForSeconds(waitingTime);

        TranslateObject?.Invoke(cube);
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
