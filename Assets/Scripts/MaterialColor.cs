using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxChangingColorAmount;

    private int _currentChangingColorAmount;
    private Renderer _renderer;

    private void Awake()
    {
        _currentChangingColorAmount = _maxChangingColorAmount;
        _renderer = GetComponent<Renderer>();
    }

    public void SetRandomColor()
    {
        if (CanChangeColor())
        {
            _currentChangingColorAmount--;

            _renderer.material.color = Random.ColorHSV();
        }
    }

    private bool CanChangeColor()
        => _currentChangingColorAmount > 0;
}