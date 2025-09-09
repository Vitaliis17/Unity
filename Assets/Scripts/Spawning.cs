using UnityEngine;
using System.Collections.Generic;

class Spawning : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private List<SpawningPoint> _spawningPoints;

    [SerializeField, Min(0)] private float _spawningTime;

    private Timer _timer;

    private void Awake()
    {
        _timer = new();
        StartCoroutine(_timer.WaitÑonstantly(_spawningTime));
    }

    private void OnEnable()
        => _timer.ConstantlyTimePassed += Spawn;

    private void OnDisable()
        => _timer.ConstantlyTimePassed -= Spawn;

    private void Spawn()
    {
        SpawningPoint spawningPoint = GetRandomSpawningPoint();

        Enemy enemy = Instantiate(_enemyPrefab, _container);
        spawningPoint.Spawn(enemy);
    }

    private SpawningPoint GetRandomSpawningPoint()
    {
        int index = Random.Range(0, _spawningPoints.Count);

        return _spawningPoints[index];
    }
}