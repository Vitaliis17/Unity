using UnityEngine;

public class Cube : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public float SeparatingChance
    {
        get
            => ReadSeparatingChance();
    }

    public void SetSize(Vector3 size)
        => transform.localScale = size;

    private void Awake()
        => Rigidbody = GetComponent<Rigidbody>();

    private float ReadSeparatingChance()
    {
        const int FullPercentAmount = 100;

        return FullPercentAmount * transform.localScale.x;
    }
}