using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour, IExplodable, IDividable, ISelectable
{
    private float _explosionForce;
    private float _explosionRadius;
    private int _divisionProbability;
    private bool _isInitialized = false;

    private MeshRenderer _mesh;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;

    public event Action<Cube> Selected;

    public Rigidbody Rigidbody => _rigidbody;
    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public Vector3 Scale => transform.localScale;
    public Vector3 Position => transform.position;
    public int DivisionProbability => _divisionProbability;
    public Vector3 LocalScale => transform.localScale;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Select()
    {
        if (_isInitialized)
        {
            SetInvisible();
            Selected?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Cube does not initialized");
        }
    }

    public void Init(Color color, int divisionProbability, float explosionForce, float explosionRadius, Vector3 localScale)
    {
        if (_isInitialized == false)
        {
            _mesh.material.color = color;
            _divisionProbability = divisionProbability;
            _explosionForce = explosionForce;
            _explosionRadius = explosionRadius;
            transform.localScale = localScale;

            _isInitialized = true;
        }
        else
        {
            Debug.LogError("Cube initialized already");
        }
    }

    private void SetInvisible()
    {
        _mesh.enabled = false;
        _boxCollider.enabled = false;
    }
}
