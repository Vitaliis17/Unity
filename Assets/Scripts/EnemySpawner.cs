using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Timer))]
class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    [SerializeField] private List<SpawnerPoint> _spawnerPoints;

    [SerializeField, Min(0)] private float _spawningTime;

    private void Awake()
        => StartCoroutine(_timer.WaitConstantly(_spawningTime));

    private void OnEnable()
        => _timer.ConstantlyTimePassed += Spawn;

    private void OnDisable()
        => _timer.ConstantlyTimePassed -= Spawn;

    private void Spawn()
    {
        SpawnerPoint spawnerPoint = GetRandomSpawnerPoint();
        spawnerPoint.Spawn();
    }

    private SpawnerPoint GetRandomSpawnerPoint()
    {
        int index = Random.Range(0, _spawnerPoints.Count);

        return _spawnerPoints[index];
    }
}