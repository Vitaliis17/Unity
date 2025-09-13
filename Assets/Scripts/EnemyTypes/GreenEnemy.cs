using UnityEngine;

public class GreenEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        GetComponent<Renderer>().material.color = Color.green;
    }
}