using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeClickHandler _cubeClickHandler;

    private readonly int _minAmount = 2;
    private readonly int _maxAmount = 6;

    public GameObject[] SpawnManyObjects(SeparatingChanceHandler cubeController)
    {
        int gameObjectAmount = Random.Range(_minAmount, _maxAmount + 1);

        GameObject[] gameObjects = new GameObject[gameObjectAmount];

        for(int i = 0; i < gameObjects.Length; i++)
            gameObjects[i] = Spawn(cubeController);

        return gameObjects;
    }

    public void DestroyCube(SeparatingChanceHandler cubeController)
        => Destroy(cubeController.gameObject);

    private GameObject Spawn(SeparatingChanceHandler cubeController)
    {
        GameObject clone = Instantiate(cubeController).gameObject;

        clone.GetComponent<Renderer>().material.color = Random.ColorHSV();   

        clone.transform.localScale = cubeController.gameObject.transform.localScale / 2;

        return clone;
    }
}