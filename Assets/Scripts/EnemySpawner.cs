using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Timer))]
class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _container;

    [SerializeField] private Timer _timer;
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private List<Transform> _spawningPoints;

    [SerializeField, Min(0)] private float _spawningTime;

    private void Awake()
        => StartCoroutine(_timer.WaitConstantly(_spawningTime));

    private void OnEnable()
        => _timer.ConstantlyTimePassed += Spawn;

    private void OnDisable()
        => _timer.ConstantlyTimePassed -= Spawn;

    private void Spawn()
    {
        Transform spawningPoint = GetRandomSpawningPoint();

        Enemy enemy = Instantiate(_enemyPrefab, _container);

        enemy.transform.position = spawningPoint.position;
    }

    private Transform GetRandomSpawningPoint()
    {
        int index = Random.Range(0, _spawningPoints.Count);

        return _spawningPoints[index];
    }
}