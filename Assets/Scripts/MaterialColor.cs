using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
        => _renderer = GetComponent<Renderer>();

    public void SetRandomColor()
        => _renderer.material.color = Random.ColorHSV();
}