using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeClickHandler _cubeClickHandler;

    private readonly int _minAmount = 2;
    private readonly int _maxAmount = 6;

    private void OnEnable() => _cubeClickHandler.CubesSpawning += SpawnManyObjects;

    private void OnDisable() => _cubeClickHandler.CubesSpawning -= SpawnManyObjects;

    private Rigidbody[] SpawnManyObjects(GameObject origin, bool isSpawnCubes)
    {
        int gameObjectAmount = isSpawnCubes ? Random.Range(_minAmount, _maxAmount + 1) : 0;

        Rigidbody[] rigidbodies = new Rigidbody[gameObjectAmount];

        for(int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i] = Spawn(origin).GetComponent<Rigidbody>();

        Destroy(origin);

        return rigidbodies;
    }

    private GameObject Spawn(GameObject origin)
    {
        GameObject clone = Instantiate(origin);

        Renderer renderer = clone.GetComponent<Renderer>();
        SetRandomColor(renderer);

        clone.transform.localScale = origin.transform.localScale / 2;

        return clone;
    }

    private void SetRandomColor(Renderer renderer)
    {
        const int ColorComponentAmount = 4;

        Color color = new();

        for (int i = 0; i < ColorComponentAmount; i++)
            color[i] = Random.Range(0f, 1f);

        renderer.material.color = color;
    }
}
