using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Timer))]
class EnemySpawning : MonoBehaviour
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

        Vector3 directionMoving = GenerateRandomDirection();
        enemy.SetDirectionMoving(directionMoving);

        enemy.transform.position = spawningPoint.position;
    }

    private Transform GetRandomSpawningPoint()
    {
        int index = Random.Range(0, _spawningPoints.Count);

        return _spawningPoints[index];
    }

    private Vector3 GenerateRandomDirection()
    {
        const int MinVectorValue = -1;
        const int MaxVectorValue = 1;

        int[] vectorValues = new int[]
        {
            MinVectorValue,
            MaxVectorValue
        };

        int index = Random.Range(0, vectorValues.Length);
        float positionX = vectorValues[index];

        index = Random.Range(0, vectorValues.Length);
        float positionZ = vectorValues[index];

        return new(positionX, 0, positionZ);
    }
}