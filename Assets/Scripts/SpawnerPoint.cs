using UnityEngine;

class SpawnerPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _purpose;

    public void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab, _container);
        enemy.SetPurpose(_purpose);

        enemy.transform.position = transform.position;
    }
}