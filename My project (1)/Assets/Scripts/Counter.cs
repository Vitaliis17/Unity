using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text _text;

    [SerializeField] private float _delay;
    [SerializeField] private float _stepValue;

    private int _clickAmount = 0;

    private void Awake()
    {
        StartCoroutine(IncreaseClickAmount());
        StartCoroutine(Timer(_delay, _stepValue));
    }

    private IEnumerator IncreaseClickAmount()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            _clickAmount++;
        }
    }

    private IEnumerator Timer(float delay, float stepValue)
    {
        float counter = 0;
        int numberParityAmount = Enum.GetValues(typeof(NumberParity)).Length;

        WaitForSeconds waitTime = new WaitForSeconds(delay);

        _text.text = counter.ToString();

        while (true)
        {
            yield return new WaitUntil(() => _clickAmount % numberParityAmount == (int)NumberParity.OddNumber);

            counter += stepValue;
            _text.text = counter.ToString();

            yield return waitTime;
        }
    }
}

public enum NumberParity
{
    evenNumber = 0,
    OddNumber
}