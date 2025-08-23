using UnityEngine;
using System;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    private float _spawnChance;

    public event Func<GameObject, bool, Rigidbody[]> CubesSpawning;
    public event Action<Rigidbody[], Vector3> CubesExploding;

    private void Awake() => _spawnChance = 100;

    private void OnEnable() => _raycaster.RaycastHitting += Spawn;

    private void OnDisable() => _raycaster.RaycastHitting -= Spawn;

    private void Spawn(GameObject cube)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        bool isSpawnCubes = UnityEngine.Random.Range(MinPercentAmount, MaxPercentAmount) <= _spawnChance;
        _spawnChance /= 2;

        Vector3 currentPosition = cube.transform.position;
        Rigidbody[] cubeRigidbodies = CubesSpawning?.Invoke(cube, isSpawnCubes);

        if(cubeRigidbodies == null || cubeRigidbodies.Length == 0) 
            return;

        CubesExploding?.Invoke(cubeRigidbodies, currentPosition);
    }
}
