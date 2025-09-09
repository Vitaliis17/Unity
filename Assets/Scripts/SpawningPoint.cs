using UnityEngine;

public class SpawningPoint : MonoBehaviour
{
    public void Spawn(Enemy enemy)
    {
        enemy.transform.position = gameObject.transform.position;

        Vector3 directionMoving = GenerateRandomDirection();
        enemy.SetDirectionMoving(directionMoving);
    }

    private Vector3 GenerateRandomDirection()
    {
        const int MinVectorValue = -1;
        const int MaxVectorValue = 1;

        int[] vectorValues = new int[]
        {
            MinVectorValue, 
            MaxVectorValue
        };

        int index = Random.Range(0, vectorValues.Length);
        float positionX = vectorValues[index];

        index = Random.Range(0, vectorValues.Length);
        float positionZ = vectorValues[index];

        return new(positionX, 0, positionZ);
    }
}
