using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _folder;

    private ObjectPool<Cube> _cubes;

    private void Awake()
        => _cubes = new ObjectPool<Cube>(Create, Get, Release, Destroy);

    private Cube Create()
        => Instantiate(_cubePrefab, _folder.transform).GetComponent<Cube>();

    private void Get(Cube cube)
        => cube.gameObject.SetActive(true);

    private void Release(Cube cube)
        => cube.gameObject.SetActive(false);

    private void Destroy(Cube cube)
        => Destroy(cube.gameObject);
}
