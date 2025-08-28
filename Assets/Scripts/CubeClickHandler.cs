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

        Cube[] cubes;
        Rigidbody[] rigidbodies;

        float radiusCoefficient = 1f;
        float forceCoefficient = 1f;

        if (isSpawnCubes)
        {
            cubes = _spawner.SpawnManyCubes(cube);
        }
        else
        {
            radiusCoefficient = cube.ExplosionRadiusMultiplier;
            forceCoefficient = cube.ExplosionForceMultiplier;

            float explosionRadius = _explosion.ReadRadius(radiusCoefficient);

            LayerMask cubeLayer = 1 << cube.gameObject.layer;
            Collider[] colliders = Physics.OverlapSphere(currentPosition, explosionRadius, cubeLayer);

            cubes = new Cube[colliders.Length];

            for (int i = 0; i < cubes.Length; i++)
                cubes[i] = colliders[i].GetComponent<Cube>();
        }

        _spawner.DestroyCube(cube);

        rigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i] = cubes[i].Rigidbody;

        _explosion.Explode(rigidbodies, currentPosition, forceCoefficient, radiusCoefficient);
    }
}
