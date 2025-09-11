using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action ConstantlyTimePassed;

    public IEnumerator WaitConstantly(float time)
    {
        WaitForSeconds waitingTime = new(time);
        
        while (enabled)
        {
            yield return waitingTime;

            ConstantlyTimePassed?.Invoke();
        }
    }
}