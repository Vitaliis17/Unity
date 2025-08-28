using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly int _minAmount = 2;
    private readonly int _maxAmount = 6;

    [SerializeField] private GameObject _cube;

    public Cube[] SpawnManyCubes(Cube cube)
    {
        int cubeAmount = Random.Range(_minAmount, _maxAmount + 1);

        Cube[] cubes = new Cube[cubeAmount];

        for(int i = 0; i < cubes.Length; i++)
            cubes[i] = Spawn(cube);

        return cubes;
    }

    public void DestroyCube(Cube cube)
        => Destroy(cube.gameObject);

    private Cube Spawn(Cube origin)
    {
        const int CloneCubeSizeDivider = 2;

        Cube cube = Instantiate(_cube).GetComponent<Cube>();

        Vector3 cubeScale = origin.transform.localScale / CloneCubeSizeDivider;
        cube.SetSize(cubeScale);

        return cube;
    }
}