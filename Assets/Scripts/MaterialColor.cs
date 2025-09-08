using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialColor : MonoBehaviour
{
    private Color _baseColor;
    private Renderer _renderer;

    public bool Swaped { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        Swaped = false;
        _renderer.material.color = _baseColor;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
        Swaped = true;
    }
}
