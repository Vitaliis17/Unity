using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _stepValue;

    [SerializeField] private KeyCode _keyCode;

    public event Action<int> ScoreChanged;

    private Coroutine _coroutine;
    private int _score = 0;

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;

                return;
            }

            _coroutine = StartCoroutine(KeepScore(_delay, _stepValue));
        }
    }

    private IEnumerator KeepScore(float delay, float stepValue)
    {
        WaitForSeconds waitTime = new(delay);

        while (true)
        {
            yield return waitTime;

            _score++;

            ScoreChanged?.Invoke(_score);
        }
    }
}