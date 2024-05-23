using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour, IExplodable, IDividable
{
    private float _explosionForce;
    private float _explosionRadius;
    private bool _isExplosionForceSetted = false;
    private bool _isExplosionRadiusSetted = false;

    private CubeFabrica _fabrica;
    private bool _isFabricaSetted = false;

    private MeshRenderer _mesh;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;

    private int _divisionProbability;
    private bool _isDivisionProbabilitySetted = false;

    public Rigidbody Rigidbody => _rigidbody;
    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public Vector3 Scale => transform.localScale;
    public Vector3 Position => transform.position;
    public int DivisionProbability => _divisionProbability;

    private void Awake()
    {
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

    public void SetExplosionForce(float force)
    {
        if (_isExplosionForceSetted == false)
        {
            _explosionForce = force;
            _isExplosionForceSetted = true;
        }
        else
        {
            Debug.LogError("ExplosionForce allready setted");
        }
    }

    public void SetExplosionRadious(float radious)
    {
        if (_isExplosionRadiusSetted == false)
        {
            _explosionRadius = radious;
            _isExplosionRadiusSetted = true;
        }
        else
        {
            Debug.LogError("ExplosionRadious allready setted");
        }
    }

    public void Explode()
    {
        if (_isFabricaSetted == false || _isDivisionProbabilitySetted == false || _isExplosionForceSetted == false)
        {
            Debug.LogError("DivisionProbability  CubeFabrica and ExplosionForce have to be setted");
            return;
        }

        SetInvisible();

        _fabrica.CreateChildCubes(this);

        List<Rigidbody> explodableRigidbodies = GetExplodableRigidbodies();

        foreach (Rigidbody explodableRigidbody in explodableRigidbodies)
        {
            explodableRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableRigidbodies()
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        if (_isExplosionRadiusSetted == false)
        {
            Debug.LogError("ExplosionRadious has to be setted");
            return rigidbodies;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform.TryGetComponent(out IExplodable explodable))
            {
                rigidbodies.Add(explodable.Rigidbody);
            }
        }

        return rigidbodies;
    }

    private void SetInvisible()
    {
        _mesh.enabled = false;
        _boxCollider.enabled = false;
    }
}
