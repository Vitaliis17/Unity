using UnityEngine;

public class TransparencySetter
{
    private readonly Renderer _renderer;

    public TransparencySetter(Renderer renderer)
        => _renderer = renderer;

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