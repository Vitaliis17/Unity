using UnityEngine;
using System;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private void OnEnable()
        => _raycaster.RaycastHitting += Spawn;

    private void OnDisable()
        => _raycaster.RaycastHitting -= Spawn;

    private void Spawn(SeparatingChanceHandler cubeController)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        bool isSpawnCubes = UnityEngine.Random.Range(MinPercentAmount, MaxPercentAmount + 1) <= cubeController.SeparatingChance;

        Vector3 currentPosition = cubeController.gameObject.transform.position;
        GameObject[] cubes = new GameObject[0];

        if (isSpawnCubes)
            cubes = _spawner.SpawnManyObjects(cubeController);

        _spawner.DestroyCube(cubeController);

        Rigidbody[] cubeRigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubeRigidbodies.Length; i++)
            cubeRigidbodies[i] = cubes[i].GetComponent<Rigidbody>();

        if (cubeRigidbodies == null || cubeRigidbodies.Length == 0)
            return;

        _explosion.Explode(cubeRigidbodies, currentPosition);
    }
}
