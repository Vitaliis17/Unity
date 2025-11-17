using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _periodicitySpawning;

    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private Exploder _exploder;
    [SerializeField] private Timer _timer;

    [SerializeField] private float _positionY;

    [SerializeField] private ValueRange<Vector2> _rangePosition;

    private Coroutine _coroutine;

    private void Start()
        => _coroutine = StartCoroutine(_timer.WaitConstantly(_periodicitySpawning));

    private void OnEnable()
    {
        _timer.ConstantlyTimeExpired += SpawnCube;
        
        _cubeSpawner.Releasing += SpawnBomb;
        _bombSpawner.Releasing += Explode;
    }

    private void OnDisable()
    {
        _timer.ConstantlyTimeExpired -= SpawnCube;
        
        _cubeSpawner.Releasing -= SpawnBomb;
        _bombSpawner.Releasing -= Explode;

        StopCoroutine(_coroutine);
    }

    private void SpawnCube()
    {
        float positionX = Random.Range(_rangePosition.Min.x, _rangePosition.Max.x);
        float positionZ = Random.Range(_rangePosition.Min.y, _rangePosition.Max.y);

        Vector3 position = new(positionX, _positionY, positionZ);

        _cubeSpawner.Spawn(position);
    }

    private void SpawnBomb(Cube cube)
        => _bombSpawner.Spawn(cube.transform.position);

    private void Explode(Bomb bomb)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bomb.ExplosionRaduis);
        
        List<Rigidbody> rigidbodies = new();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbodies.Add(rigidbody);
            }
        }

        _exploder.Explode(rigidbodies, transform.position, bomb.ExplosionForce, bomb.ExplosionRaduis);
    }
}