using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    private void Awake()
        => GetComponent<Renderer>().material.color = Random.ColorHSV();
}
