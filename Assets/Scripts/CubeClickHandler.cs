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
        Cube cube = cubeCollider.gameObject.GetComponent<Cube>();
        bool isSpawnCubes = IsSpawningCubes(cube);

        Vector3 currentPosition = cubeCollider.transform.position;

        Cube[] cubes;

        float radiusCoefficient = 1f;
        float forceCoefficient = 1f;

        float[] forceCoefficients;

        if (isSpawnCubes)
        {
            cubes = _spawner.SpawnManyCubes(cube);
            
            forceCoefficients = new float[cubes.Length];

            for(int i = 0; i < forceCoefficients.Length; i++)
                forceCoefficients[i] = _explosion.ReadForce(forceCoefficient);
        }
        else
        {
            cube.gameObject.SetActive(false);

            radiusCoefficient = cube.ExplosionRadiusMultiplier;
            float explosionRadius = _explosion.ReadRadius(radiusCoefficient);

            LayerMask cubeLayer = 1 << cube.gameObject.layer;
            Collider[] colliders = Physics.OverlapSphere(currentPosition, explosionRadius, cubeLayer);

            cubes = new Cube[colliders.Length];

            for (int i = 0; i < cubes.Length; i++)
                cubes[i] = colliders[i].GetComponent<Cube>();

            forceCoefficient = cube.ExplosionForceMultiplier;
            forceCoefficients = new float[cubes.Length];

            for (int i = 0; i < forceCoefficients.Length; i++)
                forceCoefficients[i] = _explosion.ReadForce(forceCoefficient) * ReadDistanceCoefficient(cubes[i].transform.position, currentPosition, explosionRadius);
        }

        _spawner.DestroyCube(cube);

        Rigidbody[] rigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < rigidbodies.Length; i++)
            rigidbodies[i] = cubes[i].Rigidbody;

        _explosion.ExplodeRigidbodies(rigidbodies, currentPosition, radiusCoefficient, forceCoefficients);
    }

    private bool IsSpawningCubes(Cube cube)
    {
        const int MinPercentAmount = 0;
        const int MaxPercentAmount = 100;

        return Random.Range(MinPercentAmount, MaxPercentAmount + 1) <= cube.SeparatingChance;
    }

    private float ReadDistanceCoefficient(Vector3 firstPosition, Vector3 secondPosition, float maxDistance)
    {
        const int MaxDistanceCoefficient = 1;

        return MaxDistanceCoefficient - Vector3.Distance(firstPosition, secondPosition) / maxDistance;
    }
}
