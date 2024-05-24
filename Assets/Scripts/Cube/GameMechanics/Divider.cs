using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour
{
    [SerializeField] private float _explosionForceMultiplier;
    [SerializeField] private float _explosionRadiusMultiplier;

    private float _scaleDivider = 2;
    private float _divisionProbabilityDivider = 2;

    private int _maxPartsPerCreation = 6;
    private int _minPartsPerCreation = 2;

    private void OnValidate()
    {
        _explosionForceMultiplier = Mathf.Abs(_explosionForceMultiplier);
        _explosionRadiusMultiplier = Mathf.Abs(_explosionRadiusMultiplier);
    }

    public bool TryDivide(IDividable dividable, CubeFabrica fabrica, out List<Rigidbody> dividedRigidbodies)
    {
        bool isSuccess = false;
        dividedRigidbodies = new List<Rigidbody>();

        int minProbability = 0;
        int maxProbability = 100;

        if (dividable.DivisionProbability >= Random.Range(minProbability, maxProbability))
        {
            isSuccess = true;

            int partsCount = Random.Range(_minPartsPerCreation, _maxPartsPerCreation);

            for (int i = 0; i < partsCount; i++)
            {
                IDividable dividablePart = CreateDividedParts(dividable.Position, dividable.DivisionProbability, dividable.Scale, dividable.ExplosionRadius, dividable.ExplosionForce, fabrica);
                dividedRigidbodies.Add(dividablePart.Rigidbody);
            }
        }

        return isSuccess;
    }

    private IDividable CreateDividedParts(Vector3 position, int divisionProbability, Vector3 cubeScale, float explosionRadius, float explosionForce, CubeFabrica fabrica)
    {
        Vector3 scale = cubeScale / _scaleDivider;

        Vector3 inUnitSpherePosition = Random.onUnitSphere;
        inUnitSpherePosition.y = Mathf.Abs(inUnitSpherePosition.y);
        float spawnRadius = cubeScale.magnitude;

        Vector3 spawnPosition = position + inUnitSpherePosition * spawnRadius;

        IDividable part = fabrica.Spawn(spawnPosition, (int)(divisionProbability / _divisionProbabilityDivider),
            explosionForce * _explosionForceMultiplier, explosionRadius * _explosionRadiusMultiplier, scale);

        return part;
    }
}
