using UnityEngine;

public class Patrolman : Enemy
{
    [SerializeField] private Transform[] _purposes;

    protected override void Awake()
    {
        base.Awake();

        CurrentPurpose = _purposes[0];
    }

    private void FixedUpdate()
    {
        if(IsReachePurpose())
            SetNextPurpose();
    }

    private void SetNextPurpose()
    {
        int nextIndex = 0;

        for(int i = 0; i < _purposes.Length; i++)
        {
            if (_purposes[i].Equals(CurrentPurpose))
            {
                nextIndex = i + 1;

                break;
            }
        }

        if (nextIndex == _purposes.Length)
            nextIndex = 0;

        CurrentPurpose = _purposes[nextIndex];
    }

    private bool IsReachePurpose()
    {
        const float MinDistance = 0.01f;

        Vector2 currentPosition = new(transform.position.x, transform.position.z);
        Vector2 purposePosition = new(CurrentPurpose.position.x, CurrentPurpose.position.z);

        return Vector2.Distance(currentPosition, purposePosition) <= MinDistance * Speed * Time.deltaTime;
    }
}