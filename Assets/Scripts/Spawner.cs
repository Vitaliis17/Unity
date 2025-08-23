using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeClickHandler _cubeClickHandler;

    private readonly int _minAmount = 2;
    private readonly int _maxAmount = 6;

    public GameObject[] SpawnManyObjects(Collider origin, bool isSpawnCubes)
    {
        int gameObjectAmount = isSpawnCubes ? Random.Range(_minAmount, _maxAmount + 1) : 0;

        GameObject[] rigidbodies = new GameObject[gameObjectAmount];

        for(int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i] = Spawn(origin);

        Destroy(origin.gameObject);

        return rigidbodies;
    }

    private GameObject Spawn(Collider origin)
    {
        GameObject clone = Instantiate(origin).gameObject;

        Renderer renderer = clone.GetComponent<Renderer>();
        SetRandomColor(renderer);

        clone.transform.localScale = origin.gameObject.transform.localScale / 2;

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
