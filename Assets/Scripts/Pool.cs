using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private ObjectPool<GameObject> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<GameObject>(Create);
    }

    private GameObject Create()
        => Instantiate(_cubePrefab);

    private void Get(GameObject cube)
    {
    }
}
