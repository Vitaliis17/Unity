using UnityEngine;
using System.Collections.Generic;

public class Separater : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;

    private int _minObjectAmount;
    private int _maxObjectAmount;

    private int _reductionMultiplicity;
    private float _chance;

    private void Awake()
    {
        _minObjectAmount = 2;
        _maxObjectAmount = 6;

        _reductionMultiplicity = 2;
        _chance = 100;
    }

    private void OnEnable() => _explosion.GameobjectExploding += Separate;

    private void OnDisable() => _explosion.GameobjectExploding -= Separate;

    private List<Rigidbody> Separate()
    {
        if (RandomTransmitter.ReadInt(0, 100 + 1) > _chance)
            return null;

        int separetingAmount = RandomTransmitter.ReadInt(_minObjectAmount, _maxObjectAmount + 1);

        float nextChance = _chance / _reductionMultiplicity;

        List<Rigidbody> rigidboies = new();
        GameObject cube;
        Renderer renderer;
        Rigidbody rigidbody;

        for (int i = 0; i < separetingAmount; i++)
        {
            cube = Instantiate(gameObject);

            cube.transform.localScale /= 2;

            renderer = cube.GetComponent<Renderer>();
            SetRandomColor(renderer);

            cube.GetComponent<Separater>().SetChance(nextChance);

            rigidbody = cube.GetComponent<Rigidbody>();

            if (rigidbody != null)
                rigidboies.Add(rigidbody);
        }

        return rigidboies;
    }

    private void SetRandomColor(Renderer renderer)
    {
        const int ColorComponentAmount = 4;

        Color color = new();

        for (int i = 0; i < ColorComponentAmount; i++)
            color[i] = RandomTransmitter.ReadFloat();

        renderer.material.color = color;
    }

    private void SetChance(float amount) => _chance = amount;
}
