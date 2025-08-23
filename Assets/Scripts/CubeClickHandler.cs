using UnityEngine;
using System;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private float _spawnChance;

    private void Awake() 
        => _spawnChance = 100;

    private void OnEnable() 
        => _raycaster.RaycastHitting += Spawn;

    private void OnDisable() 
        => _raycaster.RaycastHitting -= Spawn;

    private void Spawn(Collider cube)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        bool isSpawnCubes = UnityEngine.Random.Range(MinPercentAmount, MaxPercentAmount + 1) <= _spawnChance;
        _spawnChance /= 2;

        Vector3 currentPosition = cube.transform.position;

        GameObject[] cubes = _spawner.SpawnManyObjects(cube, isSpawnCubes);
        Rigidbody[] cubeRigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubeRigidbodies.Length; i++)
            cubeRigidbodies[i] = cubes[i].GetComponent<Rigidbody>();

        if(cubeRigidbodies == null || cubeRigidbodies.Length == 0) 
            return;

        _explosion.Explode(cubeRigidbodies, currentPosition);
    }
}
