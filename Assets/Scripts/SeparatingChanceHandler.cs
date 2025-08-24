using UnityEngine;

public class SeparatingChanceHandler : MonoBehaviour
{
    public float SeparatingChance
    {
        get
            => ReadSeparatingChance();
    }

    private float ReadSeparatingChance()
    {
        const int FullPercentAmount = 100;

        return FullPercentAmount * transform.localScale.x;
    }
}