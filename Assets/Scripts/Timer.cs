using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action ConstantlyTimePassed;

    public IEnumerator Wait—onstantly(float time)
    {
        WaitForSeconds waitingTime = new(time);

        while (true)
        {
            yield return waitingTime;

            ConstantlyTimePassed?.Invoke();
        }
    }
}