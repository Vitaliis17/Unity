using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform _folder;

    private ObjectPool<Cube> _cubes;

    private void Awake()
        => _cubes = new ObjectPool<Cube>(Create, Get, Release, Destroy);

    public Cube GetObject()
        => _cubes.Get();

    public void ReleaseCube(Cube cube)
        => _cubes.Release(cube);

    private Cube Create()
        => Instantiate(_objectPrefab, _folder.transform).GetComponent<Cube>();

    private void Get(Cube cube)
        => cube.gameObject.SetActive(true);

    private void Release(Cube cube)
        => cube.gameObject.SetActive(false);

    private void Destroy(Cube cube)
        => Destroy(cube.gameObject);
}
