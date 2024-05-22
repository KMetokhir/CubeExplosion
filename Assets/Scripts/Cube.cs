using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour, IExplodable
{
    [SerializeField] private float _exploisionForce = 200;
    [SerializeField] private float _explosionRadiousMultiplier = 3;

    private MeshRenderer _mesh;
    private BoxCollider _boxCollider;

    private float _explosionRadious;
    private int _divisionProbability = 100;
    private bool _isDivisionProbabilitySetted = false;

    public void SetDivisionProbability(int probability)
    {
        if (_isDivisionProbabilitySetted == false)
        {
            _divisionProbability = probability;
            _isDivisionProbabilitySetted = true;
        }
    }

    public void Explode()
    {
        SetInvisible();

        CubeFabrica fabrica = FindObjectOfType<CubeFabrica>();

        List<Rigidbody> explodableObjects;

        if (fabrica.TryCreateCubes(_divisionProbability, transform.localScale, transform.position, out explodableObjects))
        {
            foreach (Rigidbody explodableObject in explodableObjects)
            {
                explodableObject.AddExplosionForce(_exploisionForce, transform.position, _explosionRadious);
            }
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        _explosionRadious = transform.localScale.magnitude * _explosionRadiousMultiplier;

        _mesh = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();

        SetRandomColor();
    }

    private void SetInvisible()
    {
        _mesh.enabled = false;
        _boxCollider.enabled = false;
    }

    private void SetRandomColor()
    {
        Color color = new Color(Random.value, Random.value, Random.value);
        _mesh.material.color = color;
    }
}
