using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly int _minAmount = 2;
    private readonly int _maxAmount = 6;

    [SerializeField] private GameObject _cube;

    public Collider[] SpawnManyObjects(Collider origin)
    {
        int cubeAmount = Random.Range(_minAmount, _maxAmount + 1);

        Collider[] cubes = new Collider[cubeAmount];

        for(int i = 0; i < cubes.Length; i++)
            cubes[i] = Spawn(origin);

        return cubes;
    }

    public void DestroyCube(Collider cubeCollider)
        => Destroy(cubeCollider.gameObject);

    private Collider Spawn(Collider origin)
    {
        Collider cube = Instantiate(_cube).GetComponent<Collider>();  

        cube.transform.localScale = origin.transform.localScale / 2;

        return cube;
    }
}