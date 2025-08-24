using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly int _minAmount = 2;
    private readonly int _maxAmount = 6;

    public GameObject[] SpawnManyObjects(Collider cubeCollider)
    {
        int gameObjectAmount = Random.Range(_minAmount, _maxAmount + 1);

        GameObject[] gameObjects = new GameObject[gameObjectAmount];

        for(int i = 0; i < gameObjects.Length; i++)
            gameObjects[i] = Spawn(cubeCollider);

        return gameObjects;
    }

    public void DestroyCube(Collider cubeCollider)
        => Destroy(cubeCollider.gameObject);

    private GameObject Spawn(Collider cubeController)
    {
        GameObject clone = Instantiate(cubeController).gameObject;  

        clone.transform.localScale = cubeController.gameObject.transform.localScale / 2;

        return clone;
    }
}