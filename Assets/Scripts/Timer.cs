using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public Action ConstantlyTimePassed;

    public IEnumerator WaitConstantly()
    {
        WaitForFixedUpdate waitForFixedUpdate = new();

        while (true)
        {
            yield return waitForFixedUpdate;
            ConstantlyTimePassed?.Invoke();
        }
    }
}