using UnityEngine;
using TMPro;

public class CounterView : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
        => _text = GetComponent<TextMeshProUGUI>();

    public void SetText(int counter)
        => _text.text = counter.ToString();
}
