using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _conteiner;

    [SerializeField] private float _positionY;

    [SerializeField] private ValueRange<Vector2> _rangePosition;

    [field: SerializeField, Min(1f)] public float PeriodicitySpawning { get; private set; }

    private ObjectPool<Cube> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<Cube>(Create, Get, Release, Destroy);
        StartCoroutine(_timer.WaitConstantly(PeriodicitySpawning));
    }

    private void OnEnable()
        => _timer.ConstantlyTimeExpired += Spawn;

    private void OnDisable()
        => _timer.ConstantlyTimeExpired -= Spawn;

    private void Spawn()
    {
        Cube cube = _cubes.Get();

        float positionX = Random.Range(_rangePosition.MinValue.x, _rangePosition.MaxValue.x);
        float positionZ = Random.Range(_rangePosition.MinValue.y, _rangePosition.MaxValue.y);

        cube.transform.position = new(positionX, _positionY, positionZ);

        cube.Releasing += ReleaseCube;
    }

    private Cube Create()
        => Instantiate(_cubePrefab, _conteiner.transform);

    private void Get(Cube cube)
        => cube.gameObject.SetActive(true);

    private void ReleaseCube(Cube cube)
    {
        cube.Releasing -= ReleaseCube;
        _cubes.Release(cube);
    }

    private void Release(Cube cube)
        => cube.gameObject.SetActive(false);

    private void Destroy(Cube cube)
        => Destroy(cube.gameObject);
}
