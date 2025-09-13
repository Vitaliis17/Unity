using UnityEngine;

public class BlueEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        GetComponent<Renderer>().material.color = Color.blue;
    }
}