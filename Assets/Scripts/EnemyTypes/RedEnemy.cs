using UnityEngine;

public class RedEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        GetComponent<Renderer>().material.color = Color.red;
    }
}