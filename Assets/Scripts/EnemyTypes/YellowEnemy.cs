using UnityEngine;

public class YellowEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        GetComponent<Renderer>().material.color = Color.yellow;
    }
}