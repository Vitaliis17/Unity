using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    private Color _baseColor;

    public Renderer Renderer {  get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _baseColor = Renderer.material.color;
    }

    private void OnEnable()
        => Renderer.material.color = _baseColor;
}
