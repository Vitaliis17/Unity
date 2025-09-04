using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _positionY;

    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;

    [SerializeField] private float _minPositionZ;
    [SerializeField] private float _maxPositionZ;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _folder;

    private ObjectPool<Cube> _cubes;

    private void Awake()
        => _cubes = new ObjectPool<Cube>(Create, Get, Release, Destroy);

    public Cube Spawn()
    {
        Cube cube = _cubes.Get();

        float positionX = Random.Range(_minPositionX, _maxPositionX);
        float positionZ = Random.Range(_minPositionZ, _maxPositionZ);

        cube.transform.position = new(positionX, _positionY, positionZ);

        return cube;
    }

    public void ReleaseCube(Cube cube)
        => _cubes.Release(cube);

    private Cube Create()
        => Instantiate(_cubePrefab, _folder.transform).GetComponent<Cube>();

    private void Get(Cube cube)
        => cube.gameObject.SetActive(true);

    private void Release(Cube cube)
        => cube.gameObject.SetActive(false);

    private void Destroy(Cube cube)
        => Destroy(cube.gameObject);
}
