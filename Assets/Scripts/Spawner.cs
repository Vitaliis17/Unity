using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField, Min(1f)] private float periodicitySpawning;

    [SerializeField] private Timer _timer;

    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _bombContainer;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeContainer;

    [SerializeField] private float _positionY;

    [SerializeField] private ValueRange<Vector2> _rangePosition;

    private ObjectPool<Cube> _cubes;
    private ObjectPool<Bomb> _bombs;

    private void Awake()
    {
        _cubes = new ObjectPool<Cube>(CreateCube, Get, Release, Destroy);
        StartCoroutine(_timer.WaitConstantly(periodicitySpawning));

        _bombs = new ObjectPool<Bomb>(CreateBomb, Get, Release, Destroy);
    }

    private void OnEnable()
        => _timer.ConstantlyTimeExpired += SpawnCube;

    private void OnDisable()
        => _timer.ConstantlyTimeExpired -= SpawnCube;

    private void SpawnCube()
    {
        Cube cube = _cubes.Get();

        float positionX = Random.Range(_rangePosition.Min.x, _rangePosition.Max.x);
        float positionZ = Random.Range(_rangePosition.Min.y, _rangePosition.Max.y);

        cube.transform.position = new(positionX, _positionY, positionZ);

        ISpawnable spawnableObject = cube;
        spawnableObject.Releasing += ReleaseObject;
    }

    private void SpawnBomb(Vector3 position)
    {
        Bomb bomb = _bombs.Get();

        bomb.transform.position = position;
        bomb.Releasing += ReleaseObject;
    }

    private void ReleaseObject(ISpawnable spawnableObject)
    {
        spawnableObject.Releasing -= ReleaseObject;

        switch (spawnableObject)
        {
            case Cube cube:
                ReleaseCube(cube);
                break;

            case Bomb bomb:
                _bombs.Release(bomb);
                break;
        }
    }

    private void ReleaseCube(Cube cube)
    {
        _cubes.Release(cube);
        SpawnBomb(cube.transform.position);
    }



    private Bomb CreateBomb()
        => Instantiate(_bombPrefab, _bombContainer.transform);

    private void Get(Bomb bomb)
        => bomb.gameObject.SetActive(true);

    private void Release(Bomb bomb)
        => bomb.gameObject.SetActive(false);

    private void Destroy(Bomb bomb)
        => Destroy(bomb.gameObject);

    private Cube CreateCube()
        => Instantiate(_cubePrefab, _cubeContainer.transform);

    private void Get(Cube cube)
        => cube.gameObject.SetActive(true);

    private void Release(Cube cube)
        => cube.gameObject.SetActive(false);

    private void Destroy(Cube cube)
        => Destroy(cube.gameObject);
}
