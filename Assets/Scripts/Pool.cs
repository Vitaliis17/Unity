using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform _folder;

    private ObjectPool<T> _objects;

    private void Awake()
        => _objects = new ObjectPool<T>(Create, Get, Release, Destroy);

    public T GetObject()
        => _objects.Get();

    public void ReleaseObject(T component)
        => _objects.Release(component);

    private T Create()
        => Instantiate(_objectPrefab, _folder.transform).GetComponent<T>();

    private void Get(T cube)
        => cube.gameObject.SetActive(true);

    private void Release(T cube)
        => cube.gameObject.SetActive(false);

    private void Destroy(T cube)
        => Destroy(cube.gameObject);
}
