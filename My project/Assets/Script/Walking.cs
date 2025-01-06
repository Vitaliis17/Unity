using UnityEngine;

public class Walking : MonoBehaviour
{
    private void FixedUpdate()
        => transform.Translate(Time.deltaTime *  Vector3.forward, Space.Self);
}