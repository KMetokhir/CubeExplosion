using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour, IExplodable
{
    [SerializeField] private float _exploisionForce = 200;
    [SerializeField] private float _explosionRadiousMultiplier = 3;

    private CubeFabrica _fabrica;
    private bool _isFabricaSetted = false;

    private MeshRenderer _mesh;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;

    private float _explosionRadious;
    private int _divisionProbability;
    private bool _isDivisionProbabilitySetted = false;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _explosionRadious = transform.localScale.magnitude * _explosionRadiousMultiplier;

        _rigidbody = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();        
    }

    public void SetColor(Color color)
    {
        _mesh.material.color = color;
    }

    public void SetDivisionProbability(int probability)
    {
        if (_isDivisionProbabilitySetted == false)
        {
            _divisionProbability = probability;
            _isDivisionProbabilitySetted = true;
        }
        else
        {
            Debug.LogError("divisionProbability allready setted");
        }
    }

    public void SetFabrica(CubeFabrica fabrica)
    {
        if (_isFabricaSetted == false)
        {
            _fabrica = fabrica;
            _isFabricaSetted = true;
        }
        else
        {
            Debug.LogError("CubeFabrica allready setted");
        }
    }

    public void Explode()
    {
        if (_isFabricaSetted == false || _isDivisionProbabilitySetted == false)
        {
            Debug.LogError("divisionProbability and CubeFabrica have to be setted");
            return;
        }

        SetInvisible();

        List<Rigidbody> explodableObjects;

        if (_fabrica.TryCreateChildCubes(_divisionProbability, transform.localScale, transform.position, out explodableObjects))
        {
            foreach (Rigidbody explodableObject in explodableObjects)
            {
                explodableObject.AddExplosionForce(_exploisionForce, transform.position, _explosionRadious);
            }
        }

        Destroy(gameObject);
    }

    private void SetInvisible()
    {
        _mesh.enabled = false;
        _boxCollider.enabled = false;
    }    
}
