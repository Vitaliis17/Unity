using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _explosion;

    private void OnEnable()
        => _raycaster.RaycastHitting += Spawn;

    private void OnDisable()
        => _raycaster.RaycastHitting -= Spawn;

    private void Spawn(Collider cube)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        Cube handler = cube.gameObject.GetComponent<Cube>();

        bool isSpawnCubes = Random.Range(MinPercentAmount, MaxPercentAmount + 1) <= handler.SeparatingChance;

        Vector3 currentPosition = cube.gameObject.transform.position;
        Collider[] cubes = new Collider[0];

        if (isSpawnCubes)
            cubes = _spawner.SpawnManyObjects(cube);

        _spawner.DestroyCube(cube);
        Rigidbody[] cubeRigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubeRigidbodies.Length; i++)
            cubeRigidbodies[i] = cubes[i].GetComponent<Cube>().Rigidbody;

        if (cubeRigidbodies == null || cubeRigidbodies.Length == 0)
            return;

        _explosion.Explode(cubeRigidbodies, currentPosition);
    }
}
