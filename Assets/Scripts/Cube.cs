using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public float ExplosionRadiusMultiplier
    {
        get
            => 1 / transform.localScale.x;
    }

    public float ExplosionForceMultiplier
    {
        get 
            => 1 / transform.localScale.x;
    }

    public float SeparatingChance
    {
        get
            => ReadSeparatingChance();
    }

    private void Awake()
        => Rigidbody = GetComponent<Rigidbody>();

    public void SetSize(Vector3 size)
    => transform.localScale = size;

    private float ReadSeparatingChance()
    {
        const int FullPercentAmount = 100;

        return FullPercentAmount * transform.localScale.x;
    }
}