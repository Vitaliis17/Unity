using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private void OnEnable()
        => _raycaster.RaycastHitting += Spawn;

    private void OnDisable()
        => _raycaster.RaycastHitting -= Spawn;

    private void Spawn(Collider cubeCollider)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        CubeController handler = cubeCollider.gameObject.GetComponent<CubeController>();

        bool isSpawnCubes = Random.Range(MinPercentAmount, MaxPercentAmount + 1) <= handler.SeparatingChance;

        Vector3 currentPosition = cubeCollider.gameObject.transform.position;
        GameObject[] cubes = new GameObject[0];

        if (isSpawnCubes)
            cubes = _spawner.SpawnManyObjects(cubeCollider);

        _spawner.DestroyCube(cubeCollider);
        Rigidbody[] cubeRigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubeRigidbodies.Length; i++)
            cubeRigidbodies[i] = cubes[i].GetComponent<CubeController>().Rigidbody;

        if (cubeRigidbodies == null || cubeRigidbodies.Length == 0)
            return;

        _explosion.Explode(cubeRigidbodies, currentPosition);
    }
}
