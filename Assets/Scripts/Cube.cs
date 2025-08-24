using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public float SeparatingChance
    {
        get
            => ReadSeparatingChance();
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private float ReadSeparatingChance()
    {
        const int FullPercentAmount = 100;

        return FullPercentAmount * transform.localScale.x;
    }
}