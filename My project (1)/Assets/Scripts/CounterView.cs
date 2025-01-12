using UnityEngine;
using TMPro;

public class CounterView : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
        => _text = GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        Counter.ScoreChanged += SetText;
    }

    private void OnDisable()
    {
        Counter.ScoreChanged -= SetText;
    }

    private void SetText(int counter)
        => _text.text = counter.ToString();
}
