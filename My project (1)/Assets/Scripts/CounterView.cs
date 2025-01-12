using UnityEngine;
using TMPro;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private TextMeshProUGUI _text;

    private void Awake() => 
        _text = GetComponent<TextMeshProUGUI>();

    private void OnEnable() =>
        _counter.ScoreChanged += SetText;

    private void OnDisable() =>
        _counter.ScoreChanged -= SetText;

    private void SetText(int score) => 
        _text.text = score.ToString();
}
