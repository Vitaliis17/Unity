using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _stepValue;

    public UnityEvent<int> TextChanging;

    private int _clickAmount = 0;

    private void Awake()
        => StartCoroutine(Timer(_delay, _stepValue));

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button Down");

            _clickAmount++;
        }
    }

    private IEnumerator Timer(float delay, float stepValue)
    {
        int counter = 0;
        
        WaitForSeconds waitTime = new WaitForSeconds(delay);
        WaitUntil waitUntil = new WaitUntil(() => _clickAmount % 2 == 1);

        while (true)
        {
            yield return waitUntil;

            counter++;

            TextChanging?.Invoke(counter);

            yield return waitTime;
        }
    }
}