using System.Collections;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action SecondsTimeExpired;
    public event Action ConstantlyTimeExpired;

    public IEnumerator WaitSeconds(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        
        SecondsTimeExpired?.Invoke();
    }

    public IEnumerator WaitConstantly(float waitingTime)
    {
        WaitForSeconds waiting = new(waitingTime);

        while (true)
        {
            ConstantlyTimeExpired?.Invoke();

            yield return waiting;
        }
    }
}