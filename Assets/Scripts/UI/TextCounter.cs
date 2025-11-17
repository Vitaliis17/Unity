using UnityEngine;
using TMPro;

public class TextCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _value;

    private void Awake()
    {
        SetBase();
        Write();
    }

    public void Add()
    {
        _value++;
        Write();
    }

    public void Write()
        => _text.text = _value.ToString();

    public void Write(int value)
        => _text.text = value.ToString();

    private void SetBase()
    {
        const int MinValue = 0;

        _value = MinValue;
    }
}
