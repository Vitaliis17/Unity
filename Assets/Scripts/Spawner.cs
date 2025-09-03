using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;

    [SerializeField] private float _positionY;

    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;

    [SerializeField] private float _minPositionZ;
    [SerializeField] private float _maxPositionZ;

    public Cube Spawn()
    {
        Cube cube = _pool.GetObject();

        float positionX = Random.Range(_minPositionX, _maxPositionX);
        float positionZ = Random.Range(_minPositionZ, _maxPositionZ);

        cube.transform.position = new(positionX, _positionY, positionZ);

        return cube;
    }

    public void Release(Cube cube)
        => _pool.ReleaseCube(cube);
}
