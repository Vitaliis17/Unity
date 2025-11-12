using System.Collections;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action TimeExpired;
    public event Action ConstantlyTimeExpired;

    public event Action<float, float, float> OnValueChanged;

    public IEnumerator WaitSeconds(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        
        TimeExpired?.Invoke();
    }

    public IEnumerator WaitConstantly(float waitingTime)
    {
        WaitForSeconds waiting = new(waitingTime);

        while (enabled)
        {
            ConstantlyTimeExpired?.Invoke();

            yield return waiting;
        }
    }

    public IEnumerator Wait(float waitingTime)
    {
        const float MinTime = 0f;

        float fixedDeltaTime = Time.fixedDeltaTime;
        WaitForSeconds waiting = new(fixedDeltaTime);

        float maxTime = waitingTime;

        while(waitingTime > MinTime)
        {
            yield return waiting;

            waitingTime -= fixedDeltaTime;
            OnValueChanged?.Invoke(maxTime, MinTime, waitingTime);
        }

        TimeExpired?.Invoke();
    }
}