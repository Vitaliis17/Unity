using UnityEngine;

public class TransparencySetter : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private float _time;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine(_timer.Wait(_time));
    }

    private void OnEnable()
        => _timer.OnValueChanged += Set;

    private void OnDisable()
        => _timer.OnValueChanged -= Set;

    public void Set(float maxValue, float minValue, float currentValue)
    {
        float t = Mathf.InverseLerp(minValue, maxValue, currentValue);
        SetAlpha(t);
    }

    private void SetAlpha(float normalizedValue)
    {
        Color color = _renderer.material.color;
        color.a = normalizedValue;

        _renderer.material.color = color;
    }
}