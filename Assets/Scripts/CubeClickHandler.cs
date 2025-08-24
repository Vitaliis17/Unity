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

    private void Spawn(Collider cubeCollider)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        Cube cube = cubeCollider.gameObject.GetComponent<Cube>();

        bool isSpawnCubes = Random.Range(MinPercentAmount, MaxPercentAmount + 1) <= cube.SeparatingChance;

        Vector3 currentPosition = cubeCollider.gameObject.transform.position;
        Cube[] cubes = new Cube[0];

        if (isSpawnCubes)
            cubes = _spawner.SpawnManyCubes(cube);

        _spawner.DestroyCube(cube);
        Rigidbody[] cubeRigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubeRigidbodies.Length; i++)
            cubeRigidbodies[i] = cubes[i].Rigidbody;

        if (cubeRigidbodies == null || cubeRigidbodies.Length == 0)
            return;

        _explosion.Explode(cubeRigidbodies, currentPosition);
    }
}
